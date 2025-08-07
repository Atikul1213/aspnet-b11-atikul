using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Commands
{
    public class CustomerDeleteCommand : IRequest
    {
        public CustomerDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
