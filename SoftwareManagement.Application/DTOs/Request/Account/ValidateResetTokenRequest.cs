using System.ComponentModel.DataAnnotations;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}