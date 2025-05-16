using DevSkill.Inventory.Infrastructure.Identity;

namespace DevSkill.Inventory.Infrastructure.Seeds
{
    public static class RoleSeed
    {
        public static ApplicationRole[] GetRoles()
        {
            return new ApplicationRole[]
            {
                new ApplicationRole()
                {
                    Id = new Guid("F2AB8092-2370-4696-A2FB-13CAA07E4FE4"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = new DateTime(2025, 5, 19, 1, 1, 1).ToString(),
                },
                 new ApplicationRole()
                 {
                     Id = new Guid("3953C591-39C4-43A4-82F6-54E4C94AA376"),
                     Name = "Registered",
                     NormalizedName = "REGISTERED",
                     ConcurrencyStamp = new DateTime(2025, 5, 19, 1, 1, 2).ToString(),
                 },
                  new ApplicationRole()
                  {
                      Id = new Guid("D5D68B30-A048-450D-BA24-A9C035D02EA3"),
                      Name = "SuperAdmin",
                      NormalizedName = "SUPERADMIN",
                      ConcurrencyStamp = new DateTime(2025, 5, 19, 1, 1, 3).ToString(),
                  },
                  new ApplicationRole()
                  {
                      Id = new Guid("63AA3E36-F509-43B8-A322-4FF046F14600"),
                      Name = "Guest",
                      NormalizedName = "GUEST",
                      ConcurrencyStamp = new DateTime(2025, 5, 19, 1, 1, 4).ToString(),
                  }
            };
        }
    }
}
