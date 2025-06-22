using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Queries
{
    public class GetSupplierCountQuery : IRequest<IList<Supplier>>
    {
    }
}
