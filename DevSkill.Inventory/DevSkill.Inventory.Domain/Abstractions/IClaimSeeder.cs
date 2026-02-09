namespace DevSkill.Inventory.Domain.Abstractions
{
    public interface IClaimSeeder : IDataSeeder
    {
        Task SeedClaimsAsync();
    }
}
