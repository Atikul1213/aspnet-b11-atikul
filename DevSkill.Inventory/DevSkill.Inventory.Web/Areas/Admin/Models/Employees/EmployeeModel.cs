namespace DevSkill.Inventory.Web.Areas.Admin.Models.Employees
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public string NIDNumber { get; set; }
        public int StatusId { get; set; }
    }
}
