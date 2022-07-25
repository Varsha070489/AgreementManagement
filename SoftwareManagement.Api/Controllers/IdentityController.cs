

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Domain.Interfaces.Tenants;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace SoftwareManagement.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthenticationService _accountService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly ITenantProvider _tenantProvider;
        private ILogger<IdentityController> _loggerInstance;
        public IdentityController(IAuthenticationService accountService,
            IAuthenticatedUserService authenticatedUserService,
            ILogger<IdentityController> loggerInstance,
            ITenantProvider tenantProvider
            )
        {
            _accountService = accountService;
            _authenticatedUserService = authenticatedUserService;
            _loggerInstance = loggerInstance;
            _tenantProvider = tenantProvider;
        }

        /// <summary>
        /// This action is used for authenticate user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            //RegisterRequest registerRequest = new RegisterRequest();
            //registerRequest.FirstName = "Super";
            //registerRequest.LastName = "Admin";
            //registerRequest.Email = "admin@admin.com";
            //registerRequest.Password = "admin";
            //registerRequest.ConfirmPassword = "admin";
            //registerRequest.Title = "Mr.";
            //registerRequest.AcceptTerms = true;

            //await _accountService.Register(registerRequest, ipAddress());

            var response = await _accountService.Authenticate(model, ipAddress());
            return Ok(response);
        }


        /// <summary>
        /// This action is used for register user
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Register(RegisterRequest registerRequest)
        {
            await _accountService.Register(registerRequest, ipAddress());

            
            return Ok(true);
        }


        /// <summary>
        /// This action is used for verifying new created user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("verify-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest model)
        {
            await _accountService.VerifyEmail(model.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        /// <summary>
        /// This action is used for forgot password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        /// <summary>
        /// This action is temporary and used for checking authorization is working or not
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("test-data")]
        [Authorize]
        public async Task<IActionResult> TestData(ForgotPasswordRequest model)
        {
            var tenant = _tenantProvider.GetTenant();
            _loggerInstance.LogInformation("Indentity-TestData Start");
            var dd = _authenticatedUserService.UserId;
            var claims = HttpContext.User.Claims;
            foreach (var claim in claims)
            {
                System.Console.WriteLine(claim.Type + ":" + claim.Value);
            }
            return Ok(new { message = "Test Data Called Successfully" });
        }


        /// <summary>
        /// This action is temporary and used for checking authorization is working or not
        /// </summary>
         
        /// <returns></returns>
        [HttpGet]
        [Route("Get-Roles")]
        [Authorize]
        public async Task<Dictionary<int,string>> GetRoles()
        {
            var rolesReponse = _accountService.GetRoles().Result;
            Dictionary<int, string> roles = new Dictionary<int, string>();
                foreach (var item in rolesReponse)
            {
                roles.Add(item.Id, item.Name);
            }
            return roles;
        }

     



        // helper methods

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}