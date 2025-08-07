namespace DevSkill.Inventory.Web.Areas.Admin.Models.BankAccounts
{
    public class BankAccountListModel
    {
        public BankAccountListModel()
        {
            AddBankAccountModel = new AddBankAccountModel();
            UpdateBankAccountModel = new UpdateBankAccountModel();
            BankAccounts = new List<BankAccountModel>();
        }
        public AddBankAccountModel AddBankAccountModel { get; set; }
        public UpdateBankAccountModel UpdateBankAccountModel { get; set; }
        public IList<BankAccountModel> BankAccounts { get; set; }
    }
}
