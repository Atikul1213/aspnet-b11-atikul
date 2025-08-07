using DevSkill.Inventory.Domain;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Supplier
{
    public class SupplierListModel : DataTables
    {
        public SupplierListModel()
        {
            AddSupplierModel = new AddSupplierModel();
            UpdateSupplierModel = new UpdateSupplierModel();
            Suppliers = new List<SupplierModel>();
        }
        public AddSupplierModel AddSupplierModel { get; set; }
        public UpdateSupplierModel UpdateSupplierModel { get; set; }
        public IList<SupplierModel> Suppliers { get; set; }
    }
}
