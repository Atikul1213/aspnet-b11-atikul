using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface IInventoryUserRepository : IRepository<InventoryUser, Guid>
    {
        Task<(IList<InventoryUser> data, int total, int totalDisplay)> GetPagedInventoryUserAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
