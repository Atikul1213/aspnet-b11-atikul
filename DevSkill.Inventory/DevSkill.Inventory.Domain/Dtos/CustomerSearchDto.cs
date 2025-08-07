namespace DevSkill.Inventory.Domain.Dtos
{
    public class CustomerSearchDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string CompanyName { get; set; }
        public string? MobileNumber { get; set; }
        public int? BalanceFrom { get; set; }
        public int? BalanceTo { get; set; }
    }
}
