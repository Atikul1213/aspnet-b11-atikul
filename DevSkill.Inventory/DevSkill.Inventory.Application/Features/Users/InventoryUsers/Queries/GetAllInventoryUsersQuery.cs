using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetAllInventoryUsersQuery : IRequest<IList<InventoryUser>>
    {
    }
}
