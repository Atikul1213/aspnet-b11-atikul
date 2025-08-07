using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries
{
    public class GetUserRoleByIdQuery : IRequest<UserRole>
    {
        public GetUserRoleByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
