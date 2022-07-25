using System;
using System.ComponentModel.DataAnnotations;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class CreateRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        [Required]
        [Compare("PasswordHash")]
        public string ConfirmPassword { get; set; }

        public string VerificationToken { get; set; }

        public DateTime? Verified { get; set; }
        public bool AcceptTerms { get; set; }
    }
}