namespace DevSkill.Inventory.Web.Areas.Admin.Models.Sales
{
    public class SalesProductModel
    {
        public Guid Id { get; set; }
        public Guid SalesId { get; set; }
        public Guid ProductId { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public decimal MRPPrice { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public decimal SubTotal { get; set; }
    }
}
