using FinalAPI.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    /// <summary>
    /// Controller for managing email-related operations such as sending emails.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MailController));
        private readonly IMailService mailService;

        /// <summary>
        /// Constructor for initializing MailController with IMailService dependency.
        /// </summary>
        /// <param name="mailService">Mail service dependency</param>
        public MailController(IMailService mailService)
        {
            // Configure log4net
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
            this.mailService = mailService;
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="mailFrom">Sender's email address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(string mailFrom, string subject, string body)
        {
            try
            {
                await this.mailService.SendEmail(mailFrom, subject, body);
                log.Info($"Email successfully sent from: {mailFrom}, with subject: {subject}");
                return Ok();
            }
            catch (ArgumentException ex)
            {
                // Log and handle conflict
                log.Warn($"Error sending email: {ex.Message}");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                // Log and handle internal server error
                log.Error($"Internal server error while sending email: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
