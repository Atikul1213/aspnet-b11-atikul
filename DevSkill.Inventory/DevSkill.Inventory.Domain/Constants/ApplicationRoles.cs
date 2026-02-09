namespace DevSkill.Inventory.Domain.Constants
{
    public static class ApplicationRoles
    {
        public const string Admin = "Admin";
        public const string Memeber = "Member";

        public static readonly List<string> AllRoles =
        [
            Admin,
            Memeber
        ];

        public static readonly Dictionary<string, string> RoleDescription = new()
        {
            {Admin, "Administrator with full system access and management capabilities" },
            {Memeber, "Member with limited access to inventory features and functionalities" }
        };

        public static readonly Dictionary<string, List<string>> RoleClaims = new()
        {
            {
                Admin,
                new List<string>
                {
                    ApplicationClaims.CreateUser,
                    ApplicationClaims.ReadUser,
                    ApplicationClaims.UpdateUser,
                    ApplicationClaims.DeleteUser,
                    ApplicationClaims.ManageUserRoles,

                    ApplicationClaims.CreateAdmin,
                    ApplicationClaims.ReadAdmin,
                    ApplicationClaims.UpdateAdmin,
                    ApplicationClaims.DeleteAdmin,

                    ApplicationClaims.AccessAdminDashboard,
                    ApplicationClaims.ManageSystemSettings,

                    ApplicationClaims.ViewProfile,
                    ApplicationClaims.UpdateProfile,

                    ApplicationClaims.CreateProduct,
                    ApplicationClaims.ReadProduct,
                    ApplicationClaims.UpdateProduct,
                    ApplicationClaims.DeleteProduct,

                    ApplicationClaims.CreateSales,
                    ApplicationClaims.ReadSales,
                    ApplicationClaims.UpdateSales,
                    ApplicationClaims.DeleteSales
                }
            },
            {
                Memeber,
                new List<string>
                {
                    ApplicationClaims.ReadUser,
                    ApplicationClaims.ViewProducts,
                    ApplicationClaims.ViewProfile,
                    ApplicationClaims.UpdateProduct
                }
            }
        };
    }
}
