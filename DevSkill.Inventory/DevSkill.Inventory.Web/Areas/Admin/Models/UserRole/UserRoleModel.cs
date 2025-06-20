namespace DevSkill.Inventory.Web.Areas.Admin.Models.UserRole
{
    public class UserRoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Company { get; set; }
        public int StatusId { get; set; }
        public int CompanyId { get; set; }
    }
}
