using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BMICalculator.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BMICalculator.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IConfiguration configuration, ILogger<AuthService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // In a real application, you would validate against a database
            // This is just a simple example
            if (request.Username != "admin" || request.Password != "admin123")
            {
                _logger.LogWarning("Failed login attempt for user: {Username}", request.Username);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            _logger.LogInformation("User {Username} logged in successfully", request.Username);

            return new LoginResponse
            {
                Token = tokenString,
                Expiration = tokenDescriptor.Expires ?? DateTime.UtcNow.AddHours(1)
            };
        }
    }
} 