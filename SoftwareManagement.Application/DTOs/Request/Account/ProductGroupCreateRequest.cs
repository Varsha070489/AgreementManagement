using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class ProductGroupCreateRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
    public class ProductGroupDeleteRequest
    {
        public int Id { get; set; }
    }
}
