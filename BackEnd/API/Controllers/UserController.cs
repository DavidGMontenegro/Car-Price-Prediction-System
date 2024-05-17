using FinalAPI.Models;
using FinalAPI.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO; // Required for FileInfo
using System.Threading.Tasks;

namespace FinalAPI.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations such as user registration and authentication.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UsersController));
        private readonly IUserService userService;

        /// <summary>
        /// Constructor for initializing UsersController with IUserService dependency.
        /// </summary>
        /// <param name="userService">User service dependency</param>
        public UsersController(IUserService userService)
        {
            // Configure log4net
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
            this.userService = userService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="email">Email address</param>
        /// <param name="password">Password</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register(String username, String email, String password)
        {
            try
            {
                User user = new User(username, email, password);
                var registeredUser = await this.userService.RegisterUser(user);
                log.Info($"User successfully registered: {registeredUser.Username}");
                return Ok(registeredUser);
            }
            catch (ArgumentException ex)
            {
                // Log and handle conflict
                log.Warn($"Error registering user: {ex.Message}");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                // Log and handle internal server error
                log.Error($"Internal server error while registering user: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="request">Login request containing email and password</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var user = await this.userService.Authenticate(request.Email, request.Password);
                if (user != null)
                {
                    log.Info($"Successful login: {user.Username}");
                    var token = IdentityController.GenerateToken(user);
                    return Ok(new { token });
                }
                else
                {
                    log.Warn($"Invalid credentials for user: {request.Email}");
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                // Log and handle internal server error
                log.Error($"Internal server error while logging in: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves user data by username.
        /// </summary>
        /// <param name="username">Username</param>
        [HttpGet("Get-user")]
        public async Task<IActionResult> GetUserData(String username)
        {
            try
            {
                var user = await this.userService.GetUserData(username);
                if (user != null)
                {
                    log.Info($"Retrieved user data: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"User not found: {username}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log and handle internal server error
                log.Error($"Internal server error while getting user data: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Modifies user data.
        /// </summary>
        /// <param name="oldUsername">Old username</param>
        /// <param name="newUsername">New username</param>
        /// <param name="newEmail">New email address</param>
        [HttpPut("modify-user-data")]
        public async Task<IActionResult> ModifyUserData(String oldUsername, String newUsername, String newEmail)
        {
            try
            {
                var user = await this.userService.ModifyUserData(oldUsername, newUsername, newEmail);
                if (user != null)
                {
                    log.Info($"User data modified successfully: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"User not found to modify data: {oldUsername}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log and handle internal server error
                log.Error($"Internal server error while modifying user data: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Changes user password.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        [HttpPut("change-user-password")]
        public async Task<IActionResult> ChangeUserPassword(String username, String oldPassword, String newPassword)
        {
            try
            {
                var user = await this.userService.ChangeUserPassword(username, oldPassword, newPassword);
                if (user != null)
                {
                    log.Info($"User password changed successfully: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"User not found to change password: {username}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log and handle internal server error
                log.Error($"Internal server error while changing user password: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Changes user profile picture.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="newPicture">New profile picture</param>
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
                    log.Info($"User profile picture changed successfully: {user.Username}");
                    return Ok(user);
                }
                else
                {
                    log.Warn($"User not found to change profile picture: {username}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log and handle internal server error
                log.Error($"Internal server error while changing user profile picture: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
