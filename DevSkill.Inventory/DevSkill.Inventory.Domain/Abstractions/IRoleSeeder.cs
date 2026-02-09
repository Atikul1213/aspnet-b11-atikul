namespace DevSkill.Inventory.Domain.Abstractions
{
    public interface IRoleSeeder : IDataSeeder
    {
        Task SeedRoleAsync();
    }
}
