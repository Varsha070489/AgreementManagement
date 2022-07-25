

using SoftwareManagement.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareManagement.Domain.Entities.Account
{

    [Table("Identity_UserRoles")]
    public class Role : AuditEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Column(TypeName = "bit")]
        public ICollection<Account> Users { get; set; }
    }
}