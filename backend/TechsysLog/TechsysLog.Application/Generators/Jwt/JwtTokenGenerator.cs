using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Jwt;

namespace TechsysLog.Application.Generators.Jwt
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User? user, bool isDefault = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

           

            var claims = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user?.Id ?? string.Empty),
                new Claim(ClaimTypes.Email, user?.Email ?? string.Empty)
                });

            var claimsAdmin = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, "admin")
                });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = isDefault ? claimsAdmin : claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
