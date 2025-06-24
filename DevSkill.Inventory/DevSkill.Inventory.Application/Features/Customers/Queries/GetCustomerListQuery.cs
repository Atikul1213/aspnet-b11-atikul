using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Customers.Queries;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Queries
{
    public class GetCustomerListQuery : DataTables, IRequest<(IList<Customer> data, int total, int totalDisplay)>, IGetCustomerQuery
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public int? BalanceFrom { get; set; }
        public int? BalanceTo { get; set; }
    }
}
