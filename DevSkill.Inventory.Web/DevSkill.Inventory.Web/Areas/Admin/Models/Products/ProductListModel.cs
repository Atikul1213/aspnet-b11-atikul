using DevSkill.Inventory.Domain;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Products
{
    public class ProductListModel : DataTable
    {
        public ProductSearchModel SearchItem { get; set; }
    }
}
