using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Constants;
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DevSkill.Inventory.Infrastructure.Seeders
{
    public class AdminSeeder : IAdminSeeder
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminSeeder> _logger;

        #endregion

        #region Ctor
        public AdminSeeder(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILogger<AdminSeeder> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }
        #endregion

        #region Methods

        public async Task SeedAsync()
        {
            await SeedAdminUserAsync();
        }
        public async Task SeedAdminUserAsync()
        {
            try
            {
                _logger.LogInformation("Starting admin user seeding process...");

                var adminEmail = _configuration["Admin:Email"] ??
                    throw new ArgumentNullException("Admin:Email configuration is missing");
                var adminPassword = _configuration["Admin:Password"] ??
                    throw new ArgumentNullException("Admin:Password configuration is missing");
                var adminFirstName = _configuration["Admin:FirstName"] ??
                    throw new ArgumentNullException("Admin:FirstName configuration is missing");
                var adminLastName = _configuration["Admin:LastName"] ??
                    throw new ArgumentNullException("Admin:LastName configuration is missing");

                var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);
                var adminUser = new ApplicationUser();

                if (existingAdmin == null)
                {
                    adminUser.UserName = adminEmail;
                    adminUser.Email = adminEmail;
                    adminUser.EmailConfirmed = true;
                    adminUser.FirstName = adminFirstName;
                    adminUser.LastName = adminLastName;
                    adminUser.DateOfBirth = new DateTime(1999, 10, 28);
                    adminUser.PhoneNumberConfirmed = true;
                    adminUser.TwoFactorEnabled = false;
                    adminUser.LockoutEnabled = false;
                    adminUser.AccessFailedCount = 0;

                    var createResult = await _userManager.CreateAsync(adminUser, adminPassword);
                    if (createResult.Succeeded)
                    {
                        _logger.LogInformation($"Admin user created successfully with email {adminEmail}");
                    }
                }
                else
                {
                    _logger.LogInformation($"Admin user already exists with email: {adminEmail}");
                    adminUser = existingAdmin;
                }

                if (adminUser != null)
                {
                    var isAdminRole = await _userManager.IsInRoleAsync(adminUser, ApplicationRoles.Admin);
                    if (!isAdminRole)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(adminUser, ApplicationRoles.Admin);
                        if (addRoleResult.Succeeded)
                        {
                            _logger.LogInformation("Admin role added to existing user successfully.");
                        }
                        else
                        {
                            _logger.LogError($"Failed to add Admin role to existing user:" +
                                $" {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the admin user.");
            }
        }


        #endregion
    }
}
