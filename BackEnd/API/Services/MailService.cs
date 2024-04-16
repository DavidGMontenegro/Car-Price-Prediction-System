using FinalAPI.Data;
using FinalAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

public class MailService : IMailService
{
    private readonly DataContext _context;

    public MailService(DataContext context)
    {
        this._context = context;
    }
    public async Task SendEmail(string mailFrom, string subject, string body)
    {
        //setx EMAIL davidgm0928@gmail.com
        //setx PASSWORD contraseña
        //string email = Environment.GetEnvironmentVariable("EMAIL");
        //string password = Environment.GetEnvironmentVariable("PASSWORD");
        string emailFrom = "dsfinalproject@outlook.com";
        string password = "contrasenia1234";

        if (!mailFrom.Contains("@"))
        {
            // Si no contiene '@', asumimos que es un nombre de usuario
            // y buscamos el correo correspondiente en el dataContext de usuarios
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == mailFrom);

            // Si encontramos el usuario, usamos su correo electrónico
            mailFrom = user.Email;
        }

        var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailFrom, password),
        };

        var message = new MailMessage
        {
            From = new MailAddress(emailFrom),
            Subject = subject,
            Body = body,
            IsBodyHtml = true // Indica que el cuerpo del correo es HTML
        };
        message.To.Add(mailFrom);

        await smtpClient.SendMailAsync(message);
    }
}
