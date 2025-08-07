using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IBankAccountService
    {
        Task InsertBankAccountAsync(BankAccount bankAccount);
        Task UpdateBankAccountAsync(BankAccount bankAccount);
        Task DeleteBankAccountAsync(BankAccount bankAccount);
        Task<BankAccount> GetBankAccountByIdAsync(Guid id);
        Task<IList<BankAccount>> GetAllBankAccountsAsync();
    }
}
