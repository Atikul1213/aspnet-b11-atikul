using DevSkill.Inventory.Web.Models;

namespace DevSkill.Inventory.Web.Services
{
    public interface IUserInfoService
    {
        public Task<UserInfoModel> GetUserInfoAsync();
    }
}
