using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Users.InventoryUsers.Queries;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetInventoryUserListQuery : DataTables, IRequest<(IList<InventoryUser> data, int total, int totalDisplay)>, IIGetInventoryUserQuery
    {
    }
}
