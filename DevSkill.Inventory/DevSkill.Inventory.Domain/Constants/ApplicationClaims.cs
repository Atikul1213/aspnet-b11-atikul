namespace DevSkill.Inventory.Domain.Constants
{
    public static class ApplicationClaims
    {
        // User Management Claims
        public const string CreateUser = "user.create";
        public const string ReadUser = "user.read";
        public const string UpdateUser = "user.update";
        public const string DeleteUser = "user.delete";
        public const string ManageUserRoles = "user.manage_roles";

        // Admin Management Claims
        public const string CreateAdmin = "admin.create";
        public const string ReadAdmin = "admin.read";
        public const string UpdateAdmin = "admin.update";
        public const string DeleteAdmin = "admin.delete";

        // System Claims 
        public const string AccessAdminDashboard = "system.access_admin_dashboard";
        public const string AccessProductDashboard = "system.access_product_dashboard";
        public const string ManageSystemSettings = "system.manage_settings";

        // Member Claims
        public const string ViewProfile = "member.view_profile";
        public const string UpdateProfile = "member.update_profile";
        public const string ViewProducts = "member.view_product";


        // Product Management Claims
        public const string CreateProduct = "product.create";
        public const string ReadProduct = "product.read";
        public const string UpdateProduct = "product.update";
        public const string DeleteProduct = "product.delete";

        // Sales Management Claims
        public const string CreateSales = "sales.create";
        public const string ReadSales = "sales.read";
        public const string UpdateSales = "sales.update";
        public const string DeleteSales = "sales.delete";

        public static readonly Dictionary<string, string> ClaimsDescriptions = new()
        {
            { CreateUser, "Create new users" },
            { ReadUser, "View user information" },
            { UpdateUser, "Update user information" },
            { DeleteUser, "Delete users" },
            { ManageUserRoles, "Assign and remove user roles" },

            { CreateAdmin, "Create new administrators" },
            { ReadAdmin, "View administrator information" },
            { UpdateAdmin, "Update administrator information" },
            { DeleteAdmin, "Delete administrators" },

            { AccessAdminDashboard, "Access admin dashboard" },
            { AccessProductDashboard, "Access product dashboard" },
            { ManageSystemSettings, "Manage system settings" },

            { ViewProfile, "View profile" },
            { UpdateProfile, "Update profile" },
            { ViewProducts, "View products info" },

            {CreateProduct, "Create new products" },
            { ReadProduct, "View product information" },
            { UpdateProduct, "Update product information" },
            { DeleteProduct, "Delete products" },

            { CreateSales, "Create new sales records" },
            { ReadSales, "View sales information" },
            { UpdateSales, "Update sales records" },
            { DeleteSales, "Delete sales records" }
        };
    }
}
