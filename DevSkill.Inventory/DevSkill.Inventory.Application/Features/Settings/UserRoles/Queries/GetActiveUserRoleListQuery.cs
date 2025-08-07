using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries
{
    public class GetActiveUserRoleListQuery : IRequest<IList<UserRole>>
    {
    }
}
