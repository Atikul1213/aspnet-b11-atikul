using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier, Guid>
    {
        Task<(IList<Supplier> data, int total, int totalDisplay)> GetPagedSupplierAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
