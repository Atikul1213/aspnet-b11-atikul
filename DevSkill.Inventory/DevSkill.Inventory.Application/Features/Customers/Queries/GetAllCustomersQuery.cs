using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<IList<Customer>>
    {
    }
}
