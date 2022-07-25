using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class ProductCreateRequest
    {
        public int Id { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductNumber { get; set; }
        public Double Price { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProductDeleteRequest
    {
        public int Id { get; set; }
    }
}
