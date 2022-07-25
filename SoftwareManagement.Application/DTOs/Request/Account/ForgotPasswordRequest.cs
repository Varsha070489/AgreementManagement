using System.ComponentModel.DataAnnotations;

namespace SoftwareManagement.Application.DTOs.Request.Account
{ 
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}