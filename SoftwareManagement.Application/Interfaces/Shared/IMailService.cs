
using SoftwareManagement.Application.DTOs.Request.Mail;
using System.Threading.Tasks;

namespace SoftwareManagement.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}