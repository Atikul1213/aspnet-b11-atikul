using DevSkill.Inventory.Domain.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevSkill.Inventory.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        public async Task<TokenResultDto> GenerateTokenAsync(IList<Claim> claims, string key, string issuer, string audience)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResult = new TokenResultDto
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresAtUtc = tokenDescriptor.Expires ?? DateTime.UtcNow.AddMinutes(30)
            };
            return tokenResult;
        }
    }
}
