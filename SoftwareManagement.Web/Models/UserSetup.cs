using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Models
{
    public class UserSetup
    {
        
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }

        public Dictionary<int, string> Roles { get; set; }
    }
}
