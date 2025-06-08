namespace DevSkill.Inventory.Web.Areas.Admin.Models.Products
{
    public class UpdateProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public string Sku { get; set; }
    }
}
