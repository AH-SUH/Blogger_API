using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using REST_API_BloggerAPI.Data;
using REST_API_BloggerAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace REST_API_BloggerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // IConfiguration allows us to read values from appsettings.json
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Login endpoint used to create a JWT token
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Try to find a matching user based on username and password
            var user = BlogData.Users.FirstOrDefault(u =>
                u.Username == request.Username &&
                u.Password == request.Password);

            // If login info is wrong, return 401 Unauthorized
            if (user == null)
                return Unauthorized("Invalid username or password.");

            // Create claims to store inside the JWT token
            // These claims identify the user and their role
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            // Read the secret key from appsettings.json
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            // Create signing credentials using the secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Build the token with issuer, audience, claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            // Return the token as JSON
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}