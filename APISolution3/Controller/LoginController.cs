using IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APISolution3.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Ilogin _login;
        private readonly IConfiguration _configuration;


        public LoginController(Ilogin login, IConfiguration configuration)
        {
            _login = login;
            _configuration = configuration;
        }

        [HttpGet("validateuser")]
        public async Task<IActionResult> Validateuser(string username, string password)
        {
            var userdetails= await _login.Validateuser(username, password);
            var role = userdetails[0].Role;
            var massage = "";
            if (role == "Admin")
            {

                 massage = "welcome admin";
            }
            else
            {
                 massage = "welcome IAmaker";
            }

            var token = GenerateToken(username);

            return Ok(new
            {
                Token = token
            });
        }

        [NonAction]
        public string GenerateToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
