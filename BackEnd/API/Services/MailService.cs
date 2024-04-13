using FinalAPI.Services;
using System.Net;
using System.Net.Mail;

public class MailService : IMailService
{

    public Task SendEmail(string mailFrom, string subject, string body)
    {
        //setx EMAIL davidgm0928@gmail.com
        //setx PASSWORD contraseña
        string email = Environment.GetEnvironmentVariable("EMAIL");
        string password = Environment.GetEnvironmentVariable("PASSWORD");

        if (email == null || password == null)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com",587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(email, password),
            };

            return smtpClient.SendMailAsync(new MailMessage(from: "davidgm0928@gmail.com", 
                                            to: "davidgm0928@gmail.com", 
                                            subject,
                                            body));
        }else
        {
            return null;
        }
        
    }
}
