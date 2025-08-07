using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands
{
    public class UserRoleDeleteCommand : IRequest
    {
        public UserRoleDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
