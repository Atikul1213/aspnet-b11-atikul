namespace DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts
{
    public class CashAccountModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
