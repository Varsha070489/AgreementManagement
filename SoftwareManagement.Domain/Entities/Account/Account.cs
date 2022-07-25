
using SoftwareManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareManagement.Domain.Entities.Account
{

    [Table("Identity_Users")]
    public class Account : AuditEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        [Column(TypeName = "bit")]
        public bool IsVerified => Verified.HasValue;
    }
}