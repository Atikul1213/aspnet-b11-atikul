namespace DevSkill.Inventory.Domain.Entities
{
    public class Category : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
    }
}
