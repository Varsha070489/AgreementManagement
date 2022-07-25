
using SoftwareManagement.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareManagement.Domain.Entities.Settings
{
    [Table("Settings_Company")]
    public class Company : AuditEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string DisplayName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string MobileNo { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string EmailId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LandlineNo { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string WebSite { get; set; }
        [Required]
        public string CountryId { get; set; }
        [Required]
        public string StateId { get; set; }
        [Required]
        public string CityId { get; set; }
    }
}
