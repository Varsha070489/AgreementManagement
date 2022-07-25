using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.Interfaces.Shared
{
    public interface IAuthenticatedUserService
    {
        public string UserId { get; }
        public string Username { get; }
        public string HostIdentifier { get; }

    }
}
