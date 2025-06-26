namespace DevSkill.Inventory.Web.Areas.Admin.Models.BankAccounts
{
    public class BankAccountModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}
