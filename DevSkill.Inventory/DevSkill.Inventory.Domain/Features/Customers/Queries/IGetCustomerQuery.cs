namespace DevSkill.Inventory.Domain.Features.Customers.Queries
{
    public interface IGetCustomerQuery : IDataTable
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public int? BalanceFrom { get; set; }
        public int? BalanceTo { get; set; }
    }
}
