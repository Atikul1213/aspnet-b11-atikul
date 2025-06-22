namespace DevSkill.Inventory.Domain.Entities
{
    public class Customer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal OpeningBalance { get; set; }
    }
}
