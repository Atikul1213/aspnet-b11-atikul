namespace DevSkill.Inventory.Domain.Entities
{
    public class ContactUsInfo : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string OpenHours { get; set; } = string.Empty;
        public string MapEmbedUrl { get; set; } = string.Empty;
        public string HeaderTitle { get; set; } = string.Empty;
        public string HeaderSubtitle { get; set; } = string.Empty;
        public string HeaderDescription { get; set; } = string.Empty;
    }
}
