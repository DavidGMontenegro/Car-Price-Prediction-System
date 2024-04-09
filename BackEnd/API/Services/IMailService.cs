
namespace FinalAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string mailFrom, string subject, string body);
    }
}
