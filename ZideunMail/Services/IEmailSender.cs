using System.Threading.Tasks;
using ZideunMail.Models;

namespace ZideunMail.Services
{
    /// <summary>
    /// contract for email sender 
    /// </summary>
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
