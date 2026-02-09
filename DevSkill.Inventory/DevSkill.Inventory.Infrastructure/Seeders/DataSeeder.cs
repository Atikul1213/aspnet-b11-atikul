using DevSkill.Inventory.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace DevSkill.Inventory.Infrastructure.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        #region Fields

        private readonly IRoleSeeder _roleSeeder;
        private readonly IClaimSeeder _claimSeeder;
        private readonly IAdminSeeder _adminSeeder;
        private readonly ILogger _logger;

        #endregion

        #region Ctor
        public DataSeeder(IRoleSeeder roleSeeder,
            IClaimSeeder claimSeeder,
            IAdminSeeder adminSeeder,
            ILogger logger)
        {
            _roleSeeder = roleSeeder;
            _claimSeeder = claimSeeder;
            _adminSeeder = adminSeeder;
            _logger = logger;
        }

        #endregion

        #region Methods
        public async Task SeedAsync()
        {
            try
            {
                _logger.LogInformation("Starting date seeding process ...");

                await _roleSeeder.SeedAsync();
                await _claimSeeder.SeedAsync();
                await _adminSeeder.SeedAsync();

                _logger.LogInformation("Date seeding process completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during data seeding process");
            }
        }

        #endregion
    }
}
