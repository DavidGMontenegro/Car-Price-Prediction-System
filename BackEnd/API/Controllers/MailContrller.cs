using FinalAPI.Models;
using FinalAPI.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MailController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MailController));
        private readonly IMailService mailService;

        public MailController(IMailService mailService)
        {
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
            this.mailService = mailService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(string mailFrom, string subject, string body)
        {
            try
            {
                await this.mailService.SendEmail(mailFrom, subject, body);
                log.Info($"Correo electrónico enviado exitosamente desde: {mailFrom}, con asunto: {subject}");
                return Ok();
            }
            catch (ArgumentException ex)
            {
                log.Warn($"Error al enviar correo electrónico: {ex.Message}");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al enviar correo electrónico: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}