using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class BalanceTransferRepository : Repository<BalanceTransfer, Guid>, IBalanceTransferRepository
    {
        public BalanceTransferRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<(IList<BalanceTransfer> data, int total, int totalDisplay)> GetPagedBalanceTransferAsync(int pageIndex, int pageSize, string? order)
        {
            return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
        }
    }
}
