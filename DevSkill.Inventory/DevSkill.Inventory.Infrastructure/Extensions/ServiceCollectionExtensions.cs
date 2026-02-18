
using DevSkill.Core.Application;
using DevSkill.Core.Domain;
using DevSkill.Core.Infrastructure;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Repositories;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Infrastructure.Identity.Requirement;
using DevSkill.Inventory.Infrastructure.Repositories;
using DevSkill.Inventory.Infrastructure.Seeders;
using DevSkill.Inventory.Infrastructure.Seeds;
using DevSkill.Inventory.Infrastructure.Services;
using DevSkill.Inventory.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
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

            services.AddScoped<DbContextBase<ApplicationUser, ApplicationRole, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>>(provider =>
                new ApplicationDbContext(connectionString, migrationAssembly));

            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddScoped<IUnitOfWorkBase, ApplicationUnitOfWork>();

            services.AddScoped<IEmailUtility, EmailUtility>();
            services.AddScoped<IAdminSeeder, AdminSeeder>();
            services.AddScoped<IRoleSeeder, RoleSeeder>();
            services.AddScoped<IClaimSeeder, ClaimSeeder>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<ICustomUserRepository, UserRepository>();
            services.AddScoped<IServerTime, ServerTime>();
            services.AddScoped<IEmailUtility, EmailUtility>();
            services.AddSingleton<IAuthorizationHandler, AgeRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
            services.AddScoped<IUserRedirectionService, UserRedirectionService>();
            services.AddScoped<IContactUsInfoRepository, ContactUsInfoRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        #endregion

        #region Facebook Google Authentication

        public static IServiceCollection AddFacebookAuthentication(this IServiceCollection services,
            string appId, string appSecret)
        {
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = appId;
                options.AppSecret = appSecret;
                options.Scope.Add("email");
                options.Scope.Add("public_profile");
                options.Fields.Add("email");
                options.Fields.Add("name");
                options.Fields.Add("first_name");
                options.Fields.Add("last_name");
                options.Fields.Add("picture");
                options.ClaimActions.MapJsonSubKey("urn:facebook:picture", "picture", "data", "url");
            });

            return services;
        }

        public static IServiceCollection AddGoogleAuthentication(this IServiceCollection services,
            string clientId, string clientSecret)
        {
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = clientId;
                googleOptions.ClientSecret = clientSecret;
                googleOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
                googleOptions.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                googleOptions.Events.OnRemoteFailure = context =>
                {
                    context.Response.Redirect("/Account/Register");
                    context.HandleResponse();

                    return Task.CompletedTask;
                };
            });

            return services;
        }

        #endregion

        #region Add Policy
        public static void AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.Requirements.Add(new RoleRequirement("Admin"));
                });

                options.AddPolicy("MemberOnly", policy =>
                {
                    policy.Requirements.Add(new RoleRequirement("Member"));
                });
            });
        }

        #endregion
    }
}
