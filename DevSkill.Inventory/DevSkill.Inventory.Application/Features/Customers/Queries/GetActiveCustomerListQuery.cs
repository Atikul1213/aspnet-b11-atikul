using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Queries
{
    public class GetActiveCustomerListQuery : IRequest<IList<Customer>>
    {
    }
}
