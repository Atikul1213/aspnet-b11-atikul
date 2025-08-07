using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries
{
    public class GetUserRoleListQuery : IRequest<IList<UserRole>>
    {
    }
}
