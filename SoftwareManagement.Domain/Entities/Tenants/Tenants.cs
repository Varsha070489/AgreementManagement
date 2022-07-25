using SoftwareManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Entities.Tenants
{
    [Table("Tenants")]
    public class Tenant : AuditEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Host { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string APIKey { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string DatabaseConnectionString { get; set; }
    }
}
