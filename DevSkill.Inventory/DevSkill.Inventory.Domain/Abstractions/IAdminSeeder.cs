namespace DevSkill.Inventory.Domain.Abstractions
{
    public interface IAdminSeeder : IDataSeeder
    {
        Task SeedAdminUserAsync();
    }
}
