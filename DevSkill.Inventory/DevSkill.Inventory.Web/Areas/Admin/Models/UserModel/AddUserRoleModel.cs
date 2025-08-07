using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.UserModel
{
    public class AddUserRoleModel
    {
        public AddUserRoleModel()
        {
            Users = new List<SelectListItem>();
            Roles = new List<SelectListItem>();
        }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
