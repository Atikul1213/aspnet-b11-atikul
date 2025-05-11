namespace DevSkill.Inventory.Domain.Dtos
{
    public class ProductSearchDto
    {
        public string? Name { get; set; }
        public string? Sku { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
    }
}
