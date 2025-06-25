using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Products
{
    public class AddProductModel
    {
        public AddProductModel()
        {
            Categories = new List<SelectListItem>();
            Units = new List<SelectListItem>();
        }
        public string Name { get; set; }
        public string Sku { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal MRPPrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        public int LowStock { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Units { get; set; }
    }
}
