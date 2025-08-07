using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IUserRoleService
    {
        Task InsertUserRoleAsync(UserRole userRole);
        Task UpdateUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(UserRole userRole);
        Task<UserRole> GetUserRoleByIdAsync(Guid id);
        Task<IList<UserRole>> GetAllUserRolesAsync();
    }
}
