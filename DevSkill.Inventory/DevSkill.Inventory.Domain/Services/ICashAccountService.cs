using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface ICashAccountService
    {
        Task InsertCashAccountAsync(CashAccount cashAccount);
        Task UpdateCashAccountAsync(CashAccount cashAccount);
        Task DeleteCashAccountAsync(CashAccount cashAccount);
        Task<CashAccount> GetCashAccountByIdAsync(Guid id);
        Task<IList<CashAccount>> GetAllCashAccountsAsync();
    }
}
