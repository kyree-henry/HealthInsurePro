using HealthInsurePro.Application.Abstracts.Services;
using HealthInsurePro.Domain.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthInsurePro.Infrastructure.Services
{
    internal class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;
        public TokenService(IConfiguration config, DataContext context)
        {
            string? tokenKey = config["TokenKey"];
            _key = new(Encoding.UTF8.GetBytes(tokenKey!));
            _context = context;
        }

        public async Task<string> CreateToken(ApplicationUser user)
        {
            try
            {
                List<System.Security.Claims.Claim> claims = new()
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Name, user.UserName!)
                };

                SigningCredentials credentials = new(_key, SecurityAlgorithms.HmacSha512Signature);
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = credentials
                };

                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}