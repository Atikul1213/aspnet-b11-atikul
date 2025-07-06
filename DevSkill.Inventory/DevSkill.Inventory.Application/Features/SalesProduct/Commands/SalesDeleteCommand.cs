using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Commands
{
    public class SalesDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
        public SalesDeleteCommand(Guid id)
        {
            Id = id;
        }
    }
}
