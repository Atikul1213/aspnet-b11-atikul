using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Constants;
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DevSkill.Inventory.Infrastructure.Seeds
{
    public class RoleSeeder : IRoleSeeder
    {
        #region Fields

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RoleSeeder> _logger;

        #endregion

        #region Ctor
        public RoleSeeder(RoleManager<ApplicationRole> roleManager,
           ILogger<RoleSeeder> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        #endregion

        #region Methods

        public async Task SeedAsync()
        {
            await SeedRoleAsync();
        }

        public async Task SeedRoleAsync()
        {
            try
            {
                _logger.LogInformation("Starting role seeding process...");

                foreach (var roleName in ApplicationRoles.AllRoles)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        var role = new ApplicationRole
                        {
                            Name = roleName,
                            NormalizedName = roleName.ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        };

                        var result = await _roleManager.CreateAsync(role);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation($"Role {roleName} created successfully");
                        }
                    }

                    _logger.LogInformation("Role seeding precess completed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured during role seeding");
            }
        }

        #endregion
    }
}
