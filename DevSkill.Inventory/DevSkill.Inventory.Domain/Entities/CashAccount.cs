namespace DevSkill.Inventory.Domain.Entities
{
    public class CashAccount : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public decimal Balance { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
