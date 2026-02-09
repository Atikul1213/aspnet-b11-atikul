using DevSkill.Inventory.Domain.Abstractions;
using DevSkill.Inventory.Domain.Constants;
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace DevSkill.Inventory.Infrastructure.Seeds
{
    public class ClaimSeeder : IClaimSeeder
    {
        #region Fields

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        public ClaimSeeder(RoleManager<ApplicationRole> roleManager,
            ILogger logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        #endregion

        #region Methods

        public async Task SeedAsync()
        {
            await SeedClaimsAsync();
        }

        public async Task SeedClaimsAsync()
        {
            try
            {
                _logger.LogInformation("Starting claim seeding process...");

                foreach (var roleClaimPair in ApplicationRoles.RoleClaims)
                {
                    var roleName = roleClaimPair.Key;
                    var claims = roleClaimPair.Value;

                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null)
                    {
                        _logger.LogWarning($"Role {roleName} not found. Skip claim seeding for this role.");
                        continue;
                    }

                    var existingClaim = await _roleManager.GetClaimsAsync(role);
                    var existingClaimValues = existingClaim.
                        Select(c => c.Value).ToHashSet();

                    foreach (var claimValue in claims)
                    {
                        if (!existingClaimValues.Contains(claimValue))
                        {
                            var claim = new Claim("permission", claimValue);
                            var result = await _roleManager.AddClaimAsync(role, claim);

                            if (result.Succeeded)
                            {
                                _logger.LogInformation($"Claim {claimValue} added to role {role}");
                            }
                            else
                            {
                                _logger.LogError($"Failed to add claim {claimValue} to role {role}");
                            }
                        }
                    }
                }
                _logger.LogInformation("Claim seeding process completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during claim seeding");
            }
        }

        #endregion
    }
}
