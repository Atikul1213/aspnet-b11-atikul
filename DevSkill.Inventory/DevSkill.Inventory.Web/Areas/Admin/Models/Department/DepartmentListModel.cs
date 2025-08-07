namespace DevSkill.Inventory.Web.Areas.Admin.Models.Department
{
    public class DepartmentListModel
    {
        public DepartmentListModel()
        {
            AddDepartmentModel = new AddDepartmentModel();
            UpdateDepartmentModel = new UpdateDepartmentModel();
            Departments = new List<DepartmentModel>();
        }
        public AddDepartmentModel AddDepartmentModel { get; set; }
        public UpdateDepartmentModel UpdateDepartmentModel { get; set; }
        public IList<DepartmentModel> Departments { get; set; }
    }
}
