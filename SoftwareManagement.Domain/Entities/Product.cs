using SoftwareManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Entities
{
    [Table("tbl_Product")]
    public class Product : AuditEntity
    {
        public int ProductGroupId { get; set; }
        [ForeignKey(nameof(ProductGroupId))]
        public ProductGroup ProductGroup { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string ProductDescription { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductNumber { get; set; }

        public Double Price { get; set; }
    }
}
