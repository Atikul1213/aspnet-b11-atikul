namespace DevSkill.Inventory.Domain.Dtos
{
    public class UserInfoDto
    {
        public string? UserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? FullName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
