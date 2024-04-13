using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;

        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SendEmail(string mailFrom, string subject, string body)
        {
            try
            {
                await this.mailService.SendEmail(mailFrom, subject, body);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}