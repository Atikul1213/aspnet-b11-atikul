namespace DevSkill.Inventory.Web.Areas.Admin.Models.UserRole
{
    public class UserRoleListModel
    {
        public UserRoleListModel()
        {
            AddUserRoleModel = new AddUserRoleModel();
            UpdateUserRoleModel = new UpdateUserRoleModel();
            UserRoles = new List<UserRoleModel>();
        }
        public AddUserRoleModel AddUserRoleModel { get; set; }
        public UpdateUserRoleModel UpdateUserRoleModel { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}
