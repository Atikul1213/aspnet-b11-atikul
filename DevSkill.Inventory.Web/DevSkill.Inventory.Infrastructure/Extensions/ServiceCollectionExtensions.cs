
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout Settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;


                // User Settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomAccess", policy =>
                {
                    policy.RequireRole("Admin");
                    policy.RequireRole("Registered");
                });

                options.AddPolicy("ProductAddPermission", policy =>
                {
                    policy.RequireClaim("create_product", "allowed");
                });
            });
        }
    }
}
