using System.Net.Mail;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message, params Attachment[] attachments);
        Task SendEmailAsync(MailAddress recipientEmail, string subject, string message, params Attachment[] attachments);

        Task SendEmailAsync(string senderEmail, string recipientEmail, string subject, string message, params Attachment[] attachments);
        Task SendEmailAsync(MailAddress senderEmail, string recipientEmail, string subject, string message, params Attachment[] attachments);
        Task SendEmailAsync(MailAddress senderEmail, MailAddress recipientEmail, string subject, string message, params Attachment[] attachments);
    }
}
