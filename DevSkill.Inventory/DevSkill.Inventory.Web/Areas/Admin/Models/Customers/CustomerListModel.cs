namespace DevSkill.Inventory.Web.Areas.Admin.Models.Customers
{
    public class CustomerListModel
    {
        public CustomerListModel()
        {
            AddCustomerModel = new AddCustomerModel();
            UpdateCustomerModel = new UpdateCustomerModel();
            Customers = new List<CustomerModel>();
        }
        public AddCustomerModel AddCustomerModel { get; set; }
        public UpdateCustomerModel UpdateCustomerModel { get; set; }
        public IList<CustomerModel> Customers { get; set; }
    }
}
