
using DevSkill.Core.Domain;
using DevSkill.Core.Infrastructure;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Infrastructure.Identity.Requirement;
using DevSkill.Inventory.Infrastructure.Seeders;
using DevSkill.Inventory.Infrastructure.Seeds;
using DevSkill.Inventory.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Identity Services
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
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            // Configure application cookie for proper login redirection
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            //set lifespan for data protection token
            services.Configure<DataProtectionTokenProviderOptions>(
                options => options.TokenLifespan = TimeSpan.FromMinutes(10));
        }

        #endregion

        #region Dependency Injection

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,
            string connectionString, string migrationAssembly)
        {
            services.AddScoped<ApplicationDbContext>(provider =>
            new ApplicationDbContext(connectionString, migrationAssembly));

            services.AddScoped<DbContextBase<ApplicationUser, ApplicationRole,
                ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
                ApplicationRoleClaim, ApplicationUserToken>>(provider =>
                     new ApplicationDbContext(connectionString, migrationAssembly));

            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddScoped<IUnitOfWorkBase, ApplicationUnitOfWork>();

            services.AddScoped<IEmailUtility, EmailUtility>();
            services.AddScoped<IAdminSeeder, AdminSeeder>();
            services.AddScoped<IRoleSeeder, RoleSeeder>();
            services.AddScoped<IClaimSeeder, ClaimSeeder>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            return services;
        }


        #endregion


        public static void AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdminPermission", policy =>
                {
                    policy.RequireClaim("superAdmin", "superAdminAllowed");
                });

                options.AddPolicy("RegisteredPermission", policy =>
                {
                    policy.RequireClaim("registered", "registeredAllowed");
                });

                options.AddPolicy("CustomAccess", policy =>
                {
                    policy.RequireRole("Admin");
                    policy.RequireRole("Registered");
                });


                options.AddPolicy("AgeRestriction", policy =>
                {
                    policy.Requirements.Add(new AgeRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, AgeRequirementHandler>();
        }
    }
}
