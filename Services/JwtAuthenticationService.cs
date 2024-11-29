using ServiceReport.Data;
using ServiceReport.Helper;
using ServiceReport.Interface;
using ServiceReport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceReport.Services
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ServiceReportContext _context;

        public JwtAuthenticationService(IConfiguration configuration, ServiceReportContext context)
        {
            _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            _context = context;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            // Retrieve user roles
            var roles = _context.UserRole
                                .Where(ur => ur.UserId == user.Id)
                                .Select(ur => ur.Role.RoleName)
                                .ToList();

            var claims = new List<Claim>                                    
            {
                new Claim(ClaimTypes.NameIdentifier, user.EmplyoeeID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
