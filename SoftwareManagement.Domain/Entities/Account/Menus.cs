
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareManagement.Domain.Entities.Account
{
    [Table("Menus")]
    public class Menus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ControllerName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ActionName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }

    }
}
