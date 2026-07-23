namespace DevSkill.Inventory.Domain.Dtos
{
    public class TokenResultDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
        public string? RefreshToken { get; set; }
    }
}
