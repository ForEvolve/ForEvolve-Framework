using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
