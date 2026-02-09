namespace DevSkill.Inventory.Domain.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PlanName { get; set; } = null!;
        public DateTime LastLogin { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
