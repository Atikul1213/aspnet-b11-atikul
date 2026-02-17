using Cortex.Mediator.Commands;
using DevSkill.Core.Application;

namespace DevSkill.Inventory.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : ICommand<ResultResponse>
    {
        public Guid Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid UnitId { get; set; }
        public string BarcodeImagePath { get; set; }
        public decimal MRPPrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Stock { get; set; }
        public int LowStock { get; set; }
        public int DamageStock { get; set; }
        public string ImageUrl { get; set; }
    }
}
