using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IBalanceTransferService
    {
        Task InsertBalanceTransferAsync(BalanceTransfer balanceTransfer);
        Task UpdateBalanceTransferAsync(BalanceTransfer balanceTransfer);
        Task DeleteBalanceTransferAsync(BalanceTransfer balanceTransfer);
        Task<BalanceTransfer> GetBalanceTransferByIdAsync(Guid id);
        Task<IList<BalanceTransfer>> GetAllBalanceTransfersAsync();
    }
}
