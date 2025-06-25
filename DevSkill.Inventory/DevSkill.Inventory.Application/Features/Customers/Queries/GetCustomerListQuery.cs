using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Customers.Queries;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Queries
{
    public class GetCustomerListQuery : DataTables, IRequest<(IList<Customer> data, int total, int totalDisplay)>, IGetCustomerQuery
    {
        public CustomerSearchDto SearchItem { get; set; }
    }
}
