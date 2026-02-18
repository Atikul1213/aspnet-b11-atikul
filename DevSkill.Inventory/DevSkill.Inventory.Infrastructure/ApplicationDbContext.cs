using DevSkill.Core.Infrastructure;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationDbContext : DbContextBase<ApplicationUser, ApplicationRole, ApplicationUserClaim,
        ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        #region Fields

        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        #endregion

        #region Ctor
        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        #endregion

        #region Properties
        public DbSet<ContactUsInfo> ContactUsInfo { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationAssembly));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ApplicationRole>().HasData(RoleSeeder.GetRoles());
            //builder.Entity<ApplicationUserClaim>().HasData(ClaimSeed.GetClaims());
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }
        #endregion
    }
}
