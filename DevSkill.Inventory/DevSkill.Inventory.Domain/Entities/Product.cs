namespace DevSkill.Inventory.Domain.Entities
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public string ImageUrl { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal MRPPrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        public int LowStock { get; set; }
    }
}
