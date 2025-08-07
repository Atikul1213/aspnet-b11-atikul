using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        //Task<(IList<Customer> data, int total, int totalDisplay)> GetCQRSPagedCustomersAsync(int pageIndex, int pageSize, string? order, CustomerSearchDto search);
        Task<(IList<Customer> data, int total, int totalDisplay)> GetPagedCustomersAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
