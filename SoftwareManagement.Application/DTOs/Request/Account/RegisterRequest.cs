using System.ComponentModel.DataAnnotations;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class RegisterRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        
        public int RoleId { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }
    }
}