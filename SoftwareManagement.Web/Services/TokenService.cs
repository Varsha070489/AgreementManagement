
using SoftwareManagement.Web.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Services
{
    public class TokenService : ITokenService
    {
        public string JWTToken { get; set; }
    }
}
