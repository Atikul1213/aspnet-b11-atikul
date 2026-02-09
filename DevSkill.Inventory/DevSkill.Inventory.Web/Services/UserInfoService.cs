using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace DevSkill.Inventory.Web.Services
{
    public class UserInfoService : IUserInfoService
    {
        #region Fields

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Ctor
        public UserInfoService(IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        #endregion

        #region Methods

        public async Task<UserInfoModel> GetUserInfoAsync()
        {
            var model = new UserInfoModel()
            {
                IsAuthenticated = _contextAccessor.HttpContext!.User.Identity!.IsAuthenticated
            };

            if (model.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

                if (user != null)
                {
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.Email = user.Email;
                    model.ProfilePicturePath = user.ProfilePicture;
                }
            }

            return model;
        }

        #endregion
    }
}
