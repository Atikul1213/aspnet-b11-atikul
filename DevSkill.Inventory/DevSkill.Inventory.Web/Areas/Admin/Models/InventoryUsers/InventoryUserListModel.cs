namespace DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers
{
    public class InventoryUserListModel
    {
        public InventoryUserListModel()
        {
            AddInventoryUserModel = new AddInventoryUserModel();
            UpdateInventoryUserModel = new UpdateInventoryUserModel();
            InventoryUsers = new List<InventoryUserModel>();
        }
        public AddInventoryUserModel AddInventoryUserModel { get; set; }
        public UpdateInventoryUserModel UpdateInventoryUserModel { get; set; }
        public IList<InventoryUserModel> InventoryUsers { get; set; }
    }
}
