namespace DevSkill.Inventory.Domain.Entities
{
    public class UserRole : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int CompanyId { get; set; }
    }
}
