using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface IBalanceTransferRepository : IRepository<BalanceTransfer, Guid>
    {
        Task<(IList<BalanceTransfer> data, int total, int totalDisplay)> GetPagedBalanceTransferAsync(int pageIndex, int pageSize, string? order);
    }
}
