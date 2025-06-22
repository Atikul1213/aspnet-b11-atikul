using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Employees
{
    public class AddEmployeeModel
    {
        public AddEmployeeModel()
        {
            Status = new List<SelectListItem>();
            Departments = new List<SelectListItem>();
        }
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public string NIDNumber { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}
