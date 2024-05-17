using FinalAPI.Data;
using FinalAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

/// <summary>
/// Service class for sending emails.
/// </summary>
public class MailService : IMailService
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor for initializing MailService with DataContext dependency.
    /// </summary>
    /// <param name="context">Data context</param>
    public MailService(DataContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Sends an email with the specified details.
    /// </summary>
    /// <param name="mailFrom">Sender's email address or username</param>
    /// <param name="subject">Email subject</param>
    /// <param name="body">Email body</param>
    public async Task SendEmail(string mailFrom, string subject, string body)
    {
        // SMTP configuration

        //setx EMAIL email
        //setx PASSWORD contraseña
        //string email = Environment.GetEnvironmentVariable("EMAIL");
        //string password = Environment.GetEnvironmentVariable("PASSWORD");
        string emailFrom = "dsfinalproject@outlook.com";
        string password = "";

        // Check if mailFrom contains '@'; if not, assume it's a username and fetch the corresponding email from the data context
        if (!mailFrom.Contains("@"))
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == mailFrom);
            if (user != null)
                mailFrom = user.Email;
            else
                throw new ArgumentException($"Invalid sender: {mailFrom}");
        }

        // SMTP client setup
        var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailFrom, password),
        };

        // Email message setup
        var message = new MailMessage
        {
            From = new MailAddress(emailFrom),
            Subject = subject,
            Body = body,
            IsBodyHtml = true // Indicates that the email body is HTML
        };
        message.To.Add(mailFrom);

        // Send email asynchronously
        await smtpClient.SendMailAsync(message);
    }
}