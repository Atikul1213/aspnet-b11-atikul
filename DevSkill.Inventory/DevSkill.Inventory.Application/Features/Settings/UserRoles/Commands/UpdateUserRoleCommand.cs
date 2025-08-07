using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands
{
    public class UpdateUserRoleCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int CompanyId { get; set; }
    }
}
