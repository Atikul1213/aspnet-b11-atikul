using Demo.Infrastructure.Identity;

namespace Demo.Infrastructure.Seeds
{
    public static class ClaimSeed
    {
        public static ApplicationUserClaim[] GetClaims()
        {
            return new ApplicationUserClaim[]
            {
                new ApplicationUserClaim()
                {
                   Id = -1,
                   UserId = new Guid("8DB2DFB1-3150-4D72-AD44-A4D3A28DB1D1"),
                   ClaimType = "create_user",
                   ClaimValue = "allowed"
                },
            };
        }
    }
}
