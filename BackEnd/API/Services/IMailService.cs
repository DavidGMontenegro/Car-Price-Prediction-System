
namespace FinalAPI.Services
{
    public interface IMailService
    {
        Task SendEmail(string mailFrom, string subject, string body);
    }
}
