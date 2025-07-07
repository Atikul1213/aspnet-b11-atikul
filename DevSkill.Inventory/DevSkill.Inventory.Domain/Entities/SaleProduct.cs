namespace DevSkill.Inventory.Domain.Entities
{
    public class SaleProduct : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid SalesId { get; set; }
        public Guid ProductId { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public decimal MRPPrice { get; set; }
        public decimal Quantity { get; set; }
        public int Stock { get; set; }
        public decimal SubTotal { get; set; }
    }
}
