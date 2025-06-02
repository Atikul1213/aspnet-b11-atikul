using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Commands
{
    public class ProductDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
        public ProductDeleteCommand(Guid id)
        {
            Id = id;
        }
    }
}
