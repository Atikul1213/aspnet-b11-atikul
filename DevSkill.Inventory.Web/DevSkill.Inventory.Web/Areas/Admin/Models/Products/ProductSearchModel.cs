namespace DevSkill.Inventory.Web.Areas.Admin.Models.Products
{
    public class ProductSearchModel
    {
        public string? Name { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string? Sku { get; set; }
    }
}
