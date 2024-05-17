using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FinalAPI.Models;

namespace FinalAPI.Controllers
{
    /// <summary>
    /// Controller for managing identity-related operations such as user authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private const string SecretKey = "clavemuymuysecreta1234clavemuymuysecreta1234";
        private const string Issuer = "http://localhost:5110";
        private const string Audience = "http://localhost:5110";

        private static TimeSpan TokenLifetime = TimeSpan.FromHours(1);

        /// <summary>
        /// Generates a JWT token for user authentication.
        /// </summary>
        /// <param name="user">User information</param>
        [HttpPost(Name = "Login")]
        public static string GenerateToken(User user)
        {
            // Create security key and credentials
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define user claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            // Create JWT token
            var token = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                expires: DateTime.Now.Add(TokenLifetime),
                signingCredentials: credentials);

            // Write and return JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
