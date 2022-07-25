using System.ComponentModel.DataAnnotations;

namespace SoftwareManagement.Application.DTOs.Request.Account
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}