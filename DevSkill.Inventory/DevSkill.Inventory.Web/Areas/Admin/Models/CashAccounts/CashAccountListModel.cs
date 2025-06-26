namespace DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts
{
    public class CashAccountListModel
    {
        public CashAccountListModel()
        {
            AddCashAccountModel = new AddCashAccountModel();
            UpdateCashAccountModel = new UpdateCashAccountModel();
            CashAccounts = new List<CashAccountModel>();
        }
        public AddCashAccountModel AddCashAccountModel { get; set; }
        public UpdateCashAccountModel UpdateCashAccountModel { get; set; }
        public IList<CashAccountModel> CashAccounts { get; set; }
    }
}
