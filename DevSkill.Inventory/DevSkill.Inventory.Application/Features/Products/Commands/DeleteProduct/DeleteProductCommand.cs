using Cortex.Mediator.Commands;
using DevSkill.Core.Application;

namespace DevSkill.Inventory.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : ICommand<ResultResponse>
    {
        public Guid Id { get; set; }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
