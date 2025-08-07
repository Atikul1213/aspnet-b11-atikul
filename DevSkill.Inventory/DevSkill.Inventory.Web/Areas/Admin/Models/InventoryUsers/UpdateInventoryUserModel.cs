using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers
{
    public class UpdateInventoryUserModel
    {
        public UpdateInventoryUserModel()
        {
            Status = new List<SelectListItem>();
            UserRoles = new List<SelectListItem>();
            Employees = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid UserRoleId { get; set; }
        public string Password { get; set; }
        public int StatusId { get; set; }
        public string EmployeeName { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public IEnumerable<SelectListItem> UserRoles { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
