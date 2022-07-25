using SoftwareManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.DTOs.Response.Account
{
    public class AgreementResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductGroupId { get; set; }
        public int ProductId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Double ProductPrice { get; set; }
        public Double NewPrice { get; set; }
        public string Group { get; set; }
        public string ProductDescription { get; set; }
        public string GroupCode { get; set; }
        public string ProductNumber { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }

        public ProductGroup ProductGroup { get; set; }
        public Product Product { get; set; }

    }


    public class ProductResponse
    {
        public int Id { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductNumber { get; set; }
        public Double Price { get; set; }
        public string Group { get; set; }
        public bool IsActive { get; set; }
    }
    public class ProductGroupResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
