using SoftwareManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Entities
{
    [Table("tbl_Agreement")]
    public class Agreement : AuditEntity
    {
        public int UserId { get; set; }

        public int ProductGroupId { get; set; }
        [ForeignKey(nameof(ProductGroupId))]
        public ProductGroup ProductGroup { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Double ProductPrice { get; set; }
        public Double NewPrice { get; set; }
    }
}
