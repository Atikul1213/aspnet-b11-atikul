namespace DevSkill.Inventory.Web.Areas.Admin.Models.BalanceTransfers
{
    public class BalanceTransferListModel
    {
        public BalanceTransferListModel()
        {
            BalanceTransferModels = new List<BalanceTransferModel>();
            AddBalanceTransferModel = new AddBalanceTransferModel();
        }
        public AddBalanceTransferModel AddBalanceTransferModel { get; set; }
        public IList<BalanceTransferModel> BalanceTransferModels { get; set; }
    }
}
