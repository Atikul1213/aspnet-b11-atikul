namespace DevSkill.Inventory.Domain.Dtos
{
    public class ProductSearchDto
    {
        public string? Name { get; set; }
        public string? BarCode { get; set; }
        public string? Category { get; set; }
        public int? MRPFrom { get; set; }
        public int? MRPTo { get; set; }
        public int? StockFrom { get; set; }
        public int? StockTo { get; set; }
    }
}
