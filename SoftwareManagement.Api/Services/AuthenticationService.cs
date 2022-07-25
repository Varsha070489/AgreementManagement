using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


using AspNetCoreHero.Results;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.Interfaces.Account;
using SoftwareManagement.Application.DTOs.Settings;
using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Application.Exceptions;
using SoftwareManagement.Application.DTOs.Request.Mail;

namespace SoftwareManagement.Api.Services
{
    public interface IAuthenticationService
    {
        Task<Result<TokenResponse>> Authenticate(AuthenticateRequest model, string ipAddress);

        Task VerifyEmail(string token);

        Task ForgotPassword(ForgotPasswordRequest model, string origin);

        Task Register(RegisterRequest model, string origin);

        Task<List<RoleResponse>> GetRoles();

        Task<AccountResponse> GetById(int id);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private IUnitOfWorkAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly JWTSettings _jwtSettings;
        private readonly IMailService _emailService;

        public AuthenticationService(
            IMapper mapper,
            IOptions<JWTSettings> jwtSettings,
            IMailService emailService,
            IUnitOfWorkAccountService accountService)
        {
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _accountService = accountService;
        }

        public async Task<Result<TokenResponse>> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = _accountService.UserManagementServices.Accounts.SingleOrDefault(x => x.Email == model.Email);

            if (account == null || !account.IsVerified || !BC.Verify(model.Password, account.PasswordHash))
                throw new ApiException("Email or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = await GenerateJWToken(account, ipAddress);
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(account, ipAddress);
            var response = new TokenResponse();
            response.Id = account.Id.ToString();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
            response.ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
            response.Email = account.Email;
            response.UserName = account.Email;
            List<string> roles = new List<string>();

            roles.Add(_accountService.RoleServices.Roles.SingleOrDefault(x => x.Id == account.RoleId).Name);
            response.Roles = roles;
            response.IsVerified = true;
            var refreshToken = "";
            response.RefreshToken = refreshToken;
            return Result<TokenResponse>.Success(response, "Authenticated");
        }

        public async Task VerifyEmail(string token)
        {
            await Task.Run(() =>
            {
                var account = _accountService.UserManagementServices.Accounts.SingleOrDefault(x => x.VerificationToken == token);
                if (account == null) throw new AppException("Verification failed");
                account.Verified = DateTime.UtcNow;
                account.VerificationToken = null;
                UpdateRequest updateRequest = _mapper.Map<UpdateRequest>(account);
                _accountService.UserManagementServices.UpdateAsync(updateRequest);
            });
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            await Task.Run(() =>
            {
                var account = _accountService.UserManagementServices.Accounts.SingleOrDefault(x => x.Email == model.Email);

                // always return ok response to prevent email enumeration
                if (account == null) return;

                // create reset token that expires after 1 day
                account.ResetToken = randomTokenString();
                account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
                UpdateRequest updateRequest = _mapper.Map<UpdateRequest>(account);
                _accountService.UserManagementServices.UpdateAsync(updateRequest);
                // send email
                sendPasswordResetEmail(account, origin);
            });

        }

        public async Task<List<RoleResponse>> GetRoles()
        
        {
            var roles = _accountService.RoleServices.Roles.ToList();
            return roles;
        }

        public async Task Register(RegisterRequest model, string origin)
        {
            
            // validate
            var accountCheck = _accountService.UserManagementServices.Accounts;

            if (accountCheck != null && accountCheck.Count() > 0
                && accountCheck.Any(x => x.Email.ToLower() == model.Email.ToLower()))
            {
                // send already registered error in email to prevent account enumeration
                sendAlreadyRegisteredEmail(model.Email, origin);
                return;
            }

            // map model to new account object
            var account = _mapper.Map<CreateRequest>(model);

            // first registered account is an admin
            account.RoleId = 3; //model.RoleId;
            account.VerificationToken = randomTokenString();
            account.Verified = DateTime.UtcNow;

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);
            account.ConfirmPassword = BC.HashPassword(model.Password);

            // save account
            var result = await _accountService.UserManagementServices.InsertAsync(account);

            // send email
            sendVerificationEmail(account, origin);
        }
        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        // helper methods

        private async Task<AccountResponse> getAccount(int id)
        {
            var account = await _accountService.UserManagementServices.GetByIdAsync(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        

        private async Task<JwtSecurityToken> GenerateJWToken(AccountResponse user, string ipAddress)
        {
            var roleClaims = new List<Claim>();
            roleClaims.Add(new Claim("roles", user.RoleId.ToString()));
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("host", user.Email.ToString()),
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
                new Claim("full_name", $"{user.FirstName} {user.LastName}"),
                new Claim("ip", ipAddress)
            }.Union(roleClaims);
            return JWTGeneration(claims);
        }

        private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void sendVerificationEmail(CreateRequest accountResponse, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={accountResponse.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{accountResponse.VerificationToken}</code></p>";
            }

            MailRequest mailRequest = new MailRequest();
            mailRequest.To = accountResponse.Email;
            mailRequest.Subject = "Sign-up Verification API - Verify Email";
            mailRequest.Body = $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}";
            _emailService.SendAsync(mailRequest);
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";
            MailRequest mailRequest = new MailRequest();
            mailRequest.To = email;
            mailRequest.Subject = "Sign-up Verification API - Verify Email";
            mailRequest.Body = $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}";
            _emailService.SendAsync(mailRequest);
        }

        private void sendPasswordResetEmail(AccountResponse account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";
            }
            MailRequest mailRequest = new MailRequest();
            mailRequest.To = account.Email;
            mailRequest.Subject = "Sign-up Verification Email - Reset Password";
            mailRequest.Body = $@"<h4>Reset Password Email</h4>
                         {message}";
            _emailService.SendAsync(mailRequest);
        }
    }
}

