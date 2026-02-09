using DevSkill.Core.Domain;
using DevSkill.Core.Domain.Features.Membership;
using DevSkill.Core.Infrastructure.Features.Membership;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Repositories;
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class UserRepository : UserRepositoryBase<ApplicationUser,
        ApplicationRole, ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>,
        ICustomUserRepository

    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IServerTime _serverTime;

        #endregion

        #region Ctor
        public UserRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IServerTime serverTime
        ) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _serverTime = serverTime;
        }

        #endregion

        #region Methods

        public async Task<IUser> FindUserById(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new Exception("User not found");
            return user;
        }

        public async Task<UserStatus> GetUserStatusAsync(IUser user, CancellationToken cancellationToken = default)
        {
            if (user is ApplicationUser appUser)
            {
                if (appUser.LockoutEnd.HasValue && appUser.LockoutEnd > _serverTime.GetCurrentServerTime())
                    return await Task.FromResult(UserStatus.Blocked);

                return await Task.FromResult(UserStatus.Active);
            }

            return await Task.FromResult(UserStatus.Disabled);
        }

        public async Task ChangeUserStatus(IUser user, UserStatus status, CancellationToken cancellationToken = default)
        {
            if (user is ApplicationUser appUser)
            {
                switch (status)
                {
                    case UserStatus.Blocked:
                        appUser.LockoutEnd = _serverTime.GetCurrentServerTime().AddYears(100);
                        break;
                    case UserStatus.Active:
                        appUser.LockoutEnd = null;
                        break;
                    case UserStatus.Disabled:
                        break;
                }

                await _userManager.UpdateAsync(appUser);
            }
        }

        public async Task<IList<RoleDto>> GetRolesAsync()
        {
            return await _roleManager.Roles
                .Select(role => new RoleDto
                {
                    Value = role.Id.ToString(),
                    Text = role.Name
                }).ToListAsync();
        }

        public async Task<UserInfoDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;

            return new UserInfoDto
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                ProfilePictureUrl = user.ProfilePicture,
                IsActive = !(user.LockoutEnd.HasValue && user.LockoutEnd > _serverTime.GetCurrentServerTime()),
                RegisteredAt = user.CreateDate,
                LastLoginAt = user.LastLogin,
                PhoneNumber = user.PhoneNumber
            };
        }

        #endregion
    }
}
