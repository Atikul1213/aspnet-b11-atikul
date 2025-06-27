namespace DevSkill.Inventory.Web.Areas.Admin.Models.MobileAccount
{
    public class MobileAccountListModel
    {
        public MobileAccountListModel()
        {
            AddMobileAccountModel = new AddMobileAccountModel();
            UpdateMobileAccountModel = new UpdateMobileAccountModel();
            MobileAccounts = new List<MobileAccountModel>();
        }
        public AddMobileAccountModel AddMobileAccountModel { get; set; }
        public UpdateMobileAccountModel UpdateMobileAccountModel { get; set; }
        public IList<MobileAccountModel> MobileAccounts { get; set; }
    }
}
