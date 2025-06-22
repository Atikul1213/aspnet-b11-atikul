namespace DevSkill.Inventory.Web.Areas.Admin.Models.Employees
{
    public class EmployeeListModel
    {
        public EmployeeListModel()
        {
            AddEmployeeModel = new AddEmployeeModel();
            UpdateEmployeeModel = new UpdateEmployeeModel();
            Employees = new List<EmployeeModel>();
        }
        public AddEmployeeModel AddEmployeeModel { get; set; }
        public UpdateEmployeeModel UpdateEmployeeModel { get; set; }
        public IList<EmployeeModel> Employees { get; set; }
    }
}
