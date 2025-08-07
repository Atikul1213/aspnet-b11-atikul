using DevSkill.Inventory.Infrastructure.Identity;

namespace DevSkill.Inventory.Infrastructure.Seeds
{
    public static class ClaimSeed
    {
        public static ApplicationUserClaim[] GetClaims()
        {
            return new ApplicationUserClaim[]
            {
                new ApplicationUserClaim()
                {
                    Id = 1,
                    UserId = new Guid("16FED63D-5437-43A5-4C8B-08DD8F741869"),
                    ClaimType = "create_product",
                    ClaimValue = "allowed",
                }
            };
        }
    }
}
