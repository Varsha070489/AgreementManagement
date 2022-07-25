using AspNetCoreHero.Results;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftwareManagement.Application.DTOs.Request.Account;
using SoftwareManagement.Application.DTOs.Response.Account;
using SoftwareManagement.Web.Abstractions;
using SoftwareManagement.Web.Helpers;
using SoftwareManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        public AccountController()
        {

        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateRequest authenticateRequest)
        {
            var stringContent = Utility.GetContent(authenticateRequest);
            var response = await _httpClient.PostAsync<Result<TokenResponse>>("api/identity/authenticate", stringContent);
            if (response != null && response.Succeeded)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, response.Data.Email), new Claim(ClaimTypes.Role, response.Data.Roles.First()) };
                var claimsIdentity = new ClaimsIdentity(
                  claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties() { };

                await HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(claimsIdentity),
                  authProperties);
                HttpContext.Session.SetString("token", response.Data.JWToken);
              
            }
            return RedirectToAction("Index", "Agreement");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync(string emailId)
        {

            return View();
        }

        [Authorize]
        public IActionResult TestData()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> RegisterAsync()
        {
            var response = await _httpClient.GetAsync<Dictionary<int, string>>("api/identity/Get-Roles");

            return View(new UserSetup() { Roles = response });
        }



        [Authorize]
        [HttpPost]
        [Authorize(Roles = "SUPERADMIN,ADMIN")]
        public async Task<IActionResult> RegisterUserAsync(UserSetup user)
        {
            RegisterRequest request = new RegisterRequest()
            {
                AcceptTerms = true,
                ConfirmPassword = user.Password,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId,
                Title = user.Title

            };

            var stringContent = Utility.GetContent(request);
            var response = await _httpClient.PostAsync<Result<TokenResponse>>("api/identity/register", stringContent);

            var response1 = await _httpClient.GetAsync<Dictionary<int, string>>("api/identity/Get-Roles");

            return View("register",new UserSetup() { Roles = response1 });
             
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            DeleteCookies();
            return RedirectToActionPermanent("Login", "Account");
        }

        private void DeleteCookies()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
        }
    }
}
