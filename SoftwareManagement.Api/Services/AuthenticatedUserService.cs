using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SoftwareManagement.Application.Interfaces.Shared;

namespace SoftwareManagement.Api.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            HostIdentifier = httpContextAccessor.HttpContext?.User?.FindFirstValue("host");
        }

        public string UserId { get; }
        public string Username { get; }
        public string HostIdentifier { get; }
    }
}
