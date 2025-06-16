using System.Security.Claims;

namespace Demo.Infrastructure.Identity
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims, string key, string issuer, string audience);
    }
}
