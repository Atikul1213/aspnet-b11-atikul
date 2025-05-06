using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Commands
{
    public class ProductAddCommand : IRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
