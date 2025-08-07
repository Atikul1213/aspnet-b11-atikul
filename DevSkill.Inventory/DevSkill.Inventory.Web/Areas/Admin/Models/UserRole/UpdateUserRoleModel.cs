using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.UserRole
{
    public class UpdateUserRoleModel
    {
        public UpdateUserRoleModel()
        {
            Status = new List<SelectListItem>();
            Companies = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int CompanyId { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
    }
}
