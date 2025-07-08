using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface ISalesRepository : IRepository<Sales, Guid>
    {
        Task<Sales> InsertSalesAsync(Sales sales);
        Task<(IList<Sales> data, int total, int totalDisplay)> GetAllPagedSalesAsync(int pageIndex, int pageSize, string? order, SalesSearchDto search);
    }
}
