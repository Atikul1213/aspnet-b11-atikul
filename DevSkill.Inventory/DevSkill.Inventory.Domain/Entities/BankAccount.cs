namespace DevSkill.Inventory.Domain.Entities
{
    public class BankAccount : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int StatusId { get; set; }
    }
}
