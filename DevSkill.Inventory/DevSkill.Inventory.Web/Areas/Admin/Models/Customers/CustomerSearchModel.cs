namespace DevSkill.Inventory.Web.Areas.Admin.Models.Customers
{
    public class CustomerSearchModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string CompanyName { get; set; }
        public string? MobileNumber { get; set; }
        public int? BalanceFrom { get; set; }
        public int? BalanceTo { get; set; }
    }
}
