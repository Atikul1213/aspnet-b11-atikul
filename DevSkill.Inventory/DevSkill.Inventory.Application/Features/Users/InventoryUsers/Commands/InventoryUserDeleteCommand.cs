using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands
{
    public class InventoryUserDeleteCommand : IRequest
    {
        public InventoryUserDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
