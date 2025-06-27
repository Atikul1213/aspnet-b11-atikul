using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IMobileAccountService
    {
        Task InsertMobileAccountAsync(MobileAccount mobileAccount);
        Task UpdateMobileAccountAsync(MobileAccount mobileAccount);
        Task DeleteMobileAccountAsync(MobileAccount mobileAccount);
        Task<MobileAccount> GetMobileAccountByIdAsync(Guid id);
        Task<IList<MobileAccount>> GetAllMobileAccountsAsync();
    }
}
