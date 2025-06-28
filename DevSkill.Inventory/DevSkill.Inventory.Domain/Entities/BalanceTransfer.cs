namespace DevSkill.Inventory.Domain.Entities
{
    public class BalanceTransfer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FromAccountName { get; set; }
        public string ToAccountName { get; set; }
        public int SendingAccountTypeId { get; set; }
        public int ReceiveAccountTypeId { get; set; }
        public Guid SendingAccountId { get; set; }
        public Guid ReceiveAccountId { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime TransferDate { get; set; }
        public string Note { get; set; }
    }
}
