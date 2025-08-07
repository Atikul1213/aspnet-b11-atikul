using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IInventoryUserService
    {
        Task InsertInventoryUserAsync(InventoryUser supplier);
        Task UpdateInventoryUserAsync(InventoryUser supplier);
        Task DeleteInventoryUserAsync(InventoryUser supplier);
        Task<InventoryUser> GetInventoryUserByIdAsync(Guid id);
        Task<IList<InventoryUser>> GetAllInventoryUsersAsync();
    }
}
