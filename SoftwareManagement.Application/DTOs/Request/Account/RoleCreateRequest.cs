using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class RoleCreateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
    public class RoleDeleteRequest
    {
        public int Id { get; set; }
    }
}
