using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Commands
{
    public class ProductUpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateOnUtc { get; set; }
    }
}
