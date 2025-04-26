using Demo.Infrastructure.Identity;

namespace Demo.Infrastructure.Seeds
{
    public static class RoleSeed
    {
        public static ApplicationRole[] GetRoles()
        {

            return [
                new ApplicationRole
                {
                    Id = new Guid("8ACF9875-73D7-4A67-921A-E28B3AF34E71"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = new DateTime(2025, 4,19, 1, 2,1).ToString()
                },
                new ApplicationRole
                {
                     Id = new Guid("CD98DC1C-4F83-493C-A11D-1DD9D3528322"),
                    Name = "HR",
                    NormalizedName = "HR",
                    ConcurrencyStamp = new DateTime(2025, 4,19, 1, 2,2).ToString()
                },
                new ApplicationRole
                 {
                     Id = new Guid("5457D1C5-BC6E-4EF7-BC4F-B86A0B012DE1"),
                     Name = "Author",
                     NormalizedName = "AUTHOR",
                     ConcurrencyStamp = new DateTime(2025, 4,19, 1, 2,3).ToString()
                 }
                ];
        }
    }
}
