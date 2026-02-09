using DevSkill.Core.Domain.Features.Membership;
using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface ICustomUserRepository : IUserRepository
    {
        Task<IUser> FindUserById(Guid id, CancellationToken cancellationToken = default);
        Task<UserStatus> GetUserStatusAsync(IUser user, CancellationToken cancellationToken = default);
        Task ChangeUserStatus(IUser user, UserStatus status, CancellationToken cancellationToken = default);
        Task<IList<RoleDto>> GetRolesAsync();
        Task<UserInfoDto?> GetUserByIdAsync(Guid userId);
    }
}
