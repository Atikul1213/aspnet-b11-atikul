using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid, ApplicationUserClaim,
        ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
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

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InventoryUser> InventoryUser { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CashAccount> CashAccounts { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<MobileAccount> MobileAccounts { get; set; }
        public DbSet<BalanceTransfer> BalanceTransfers { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
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
            builder.Entity<ApplicationRole>().HasData(RoleSeed.GetRoles());
            //builder.Entity<ApplicationUserClaim>().HasData(ClaimSeed.GetClaims());

            base.OnModelCreating(builder);
        }
        #endregion
    }
}
