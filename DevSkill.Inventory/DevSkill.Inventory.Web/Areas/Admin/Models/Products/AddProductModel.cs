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
        public string BarCode { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        [ValidateNever]
        public string CategoryName { get; set; }
        public Guid UnitId { get; set; }
        public decimal MRPPrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        [ValidateNever]
        public string BarcodeImagePath { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Stock { get; set; }
        public int LowStock { get; set; }
        public int DamageStock { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Units { get; set; }
    }
}
