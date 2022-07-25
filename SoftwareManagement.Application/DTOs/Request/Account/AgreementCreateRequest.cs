using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class AgreementCreateRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int ProductGroupId { get; set; }
        public int ProductId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Double ProductPrice { get; set; }
        public Double NewPrice { get; set; }
        public bool IsActive { get; set; }
      
    }

    public class AgreementDeleteRequest
    {
        public int Id { get; set; }
    }
}
