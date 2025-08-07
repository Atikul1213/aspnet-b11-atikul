using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SupplierRepository : Repository<Supplier, Guid>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IList<Supplier> data, int total, int totalDisplay)> GetPagedSupplierAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            if (string.IsNullOrEmpty(search.Value))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            else
                return await GetDynamicAsync(x => x.Name.Contains(search.Value), order, null, pageIndex, pageSize, true);
        }
    }
}
