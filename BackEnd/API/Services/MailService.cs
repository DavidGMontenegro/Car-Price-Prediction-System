using FinalAPI.Services;
using System.Net;
using System.Net.Mail;

public class MailService : IMailService
{
    private readonly SmtpClient _smtpClient;

    public Task SendEmailAsync(string mailFrom, string subject, string body)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("username", "password"),
            EnableSsl = true,
        };

        smtpClient.Send("email", "recipient", "subject", "body");
        return Task.FromResult(0);
    }
}
