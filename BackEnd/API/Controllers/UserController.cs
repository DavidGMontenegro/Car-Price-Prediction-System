using FinalAPI.Models;
using FinalAPI.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UsersController));

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(String username, String email, String password)
        {
            try
            {
                User user = new User(username, email, password);
                var registeredUser = await this.userService.RegisterUser(user);
                log.Info($"Usuario registrado exitosamente: {registeredUser.Username}");
                return Ok(registeredUser);
            }
            catch (ArgumentException ex)
            {
                log.Warn($"Error al registrar usuario: {ex.Message}");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al registrar usuario: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(String username, String password)
        {
            try
            {
                var user = await this.userService.Authenticate(username, password);
                if (user != null)
                {
                    log.Info($"Inicio de sesión exitoso: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"Credenciales inválidas para el usuario: {username}");
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al iniciar sesión: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("Get-user")]
        public async Task<IActionResult> GetUserData(String username)
        {
            try
            {
                var user = await this.userService.GetUserData(username);
                if (user != null)
                {
                    log.Info($"Obtenidos datos del usuario: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"Usuario no encontrado: {username}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al obtener datos del usuario: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("modify-user-data")]
        public async Task<IActionResult> ModifyUserData(String oldUsername, String newUsername, String newEmail)
        {
            try
            {
                var user = await this.userService.ModifyUserData(oldUsername, newUsername, newEmail);
                if (user != null)
                {
                    log.Info($"Datos de usuario modificados exitosamente: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"Usuario no encontrado para modificar datos: {oldUsername}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al modificar datos del usuario: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("change-user-password")]
        public async Task<IActionResult> ChangeUserPassword(String username, String oldPassword, String newPassword)
        {
            try
            {
                var user = await this.userService.ChangeUserPassword(username, oldPassword, newPassword);
                if (user != null)
                {
                    log.Info($"Contraseña de usuario cambiada exitosamente: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"Usuario no encontrado para cambiar contraseña: {username}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al cambiar contraseña del usuario: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("change-user-picture")]
        public async Task<IActionResult> ChangeUserProfilePicture(string username, IFormFile newPicture)
        {
            try
            {
                byte[] newProfilePic;
                using (var memoryStream = new MemoryStream())
                {
                    await newPicture.CopyToAsync(memoryStream);
                    newProfilePic = memoryStream.ToArray();
                }

                var user = await this.userService.ChangeUserProfilePic(username, newProfilePic);
                if (user != null)
                {
                    log.Info($"Imagen de perfil del usuario cambiada exitosamente: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"Usuario no encontrado para cambiar imagen de perfil: {username}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al cambiar imagen de perfil del usuario: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
