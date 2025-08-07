namespace DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers
{
    public class InventoryUserModel
    {
        public Guid Id { get; set; }
        public int StatusId { get; set; }
        public string EmployeeName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
