using DevSkill.Inventory.Domain;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Products
{
    public class ProductListModel : DataTables
    {
        public ProductListModel()
        {
            AddProductModel = new AddProductModel();
            SearchItem = new ProductSearchModel();
        }
        public ProductSearchModel SearchItem { get; set; }
        public AddProductModel AddProductModel { get; set; }
    }
}
