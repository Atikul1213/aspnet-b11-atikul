using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Products.Query;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task<(IList<Customer> data, int total, int totalDisplay)> GetCQRSPagedCustomersAsync(IGetProductQuery request);
    }
}
