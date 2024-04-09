using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FinalAPI.Models;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private const string SecretKey = "clavemuymuysecreta1234clavemuymuysecreta1234";
        private const string Issuer = "http://localhost:5110";
        private const string Audience = "http://localhost:5110";

        private static TimeSpan TokenLifetime = TimeSpan.FromHours(1);

        [HttpPost(Name = "Login")]
        public ActionResult GenerateToken(LoginRequest request)
        {
            if (!CheckUserAndPass())
                return Unauthorized();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var SecretToken = new JwtSecurityToken(
              Issuer,
              Audience,
              null,
              expires: DateTime.Now.Add(TokenLifetime),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(SecretToken);

            return Ok(token);
        }

        private bool CheckUserAndPass()
        {
            return true;
        }
    }
}
