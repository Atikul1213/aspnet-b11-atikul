using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class InventoryUserRepository : Repository<InventoryUser, Guid>, IInventoryUserRepository
    {
        public InventoryUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IList<InventoryUser> data, int total, int totalDisplay)> GetPagedInventoryUserAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            if (string.IsNullOrEmpty(search.Value))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            else
                return await GetDynamicAsync(x => x.EmployeeName.Contains(search.Value), order, null, pageIndex, pageSize, true);
        }
    }
}
