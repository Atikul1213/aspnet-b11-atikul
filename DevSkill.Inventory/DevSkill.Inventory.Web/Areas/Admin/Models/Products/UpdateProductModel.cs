using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Products
{
    public class UpdateProductModel
    {
        public UpdateProductModel()
        {
            Categories = new List<SelectListItem>();
            Units = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int UnitId { get; set; }
        public decimal MRPPrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Stock { get; set; }
        public int LowStock { get; set; }
        public int DamageStock { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Units { get; set; }
    }
}
