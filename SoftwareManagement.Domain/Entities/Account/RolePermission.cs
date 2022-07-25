
using SoftwareManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareManagement.Domain.Entities.Account
{
    [Table("Identity_UserRolePermission")]
    public class RolePermission : AuditEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ModuleName { get; set; }
        public int RoleId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string PermissionName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string PermissionDisplayName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string HelpText { get; set; }
        public bool View { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }
}
