using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetInventoryUserByEmailQuery : IRequest<IList<InventoryUser>>
    {
        public GetInventoryUserByEmailQuery(string email)
        {
            Email = email;
        }
        public string Email { get; set; }
    }
}
