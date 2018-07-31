using System.Net.Mail;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message);
        Task SendEmailAsync(MailAddress recipientEmail, string subject, string message);

        Task SendEmailAsync(string senderEmail, string recipientEmail, string subject, string message);
        Task SendEmailAsync(MailAddress senderEmail, string recipientEmail, string subject, string message);
        Task SendEmailAsync(MailAddress senderEmail, MailAddress recipientEmail, string subject, string message);
    }
}
