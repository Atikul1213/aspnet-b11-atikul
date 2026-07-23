using DevSkill.Inventory.Domain.Dtos;
using System.Security.Claims;

namespace DevSkill.Inventory.Infrastructure.Services
{
    public interface IJwtTokenService
    {
        Task<TokenResultDto> GenerateTokenAsync(IList<Claim> claims, string key, string issuer, string audience);
    }
}
