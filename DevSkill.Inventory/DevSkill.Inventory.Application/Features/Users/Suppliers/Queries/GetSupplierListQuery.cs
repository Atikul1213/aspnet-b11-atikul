using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Users.Suppliers.Queries;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Queries
{
    public class GetSupplierListQuery : DataTables, IRequest<(IList<Supplier> data, int total, int totalDisplay)>, IGetSupplierQuery
    {
    }
}
