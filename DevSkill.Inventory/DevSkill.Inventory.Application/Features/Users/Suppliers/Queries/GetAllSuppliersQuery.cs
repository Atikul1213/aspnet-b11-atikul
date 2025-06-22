using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Queries
{
    public class GetAllSuppliersQuery : IRequest<IList<Supplier>>
    {
    }
}
