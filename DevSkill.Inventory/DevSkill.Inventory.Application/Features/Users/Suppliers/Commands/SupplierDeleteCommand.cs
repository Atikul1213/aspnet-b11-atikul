using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Commands
{
    public class SupplierDeleteCommand : IRequest
    {
        public SupplierDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
