using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Api.Models.JwtToken.Dto
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
        public string? RefreshToken { get; set; }
        public UserInfoDto User { get; set; } = default!;
    }
}
