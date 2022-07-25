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
    [Table("tbl_ProductGroup")]
    public class ProductGroup : AuditEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
    }
}
