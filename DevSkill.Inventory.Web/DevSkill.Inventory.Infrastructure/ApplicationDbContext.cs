using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public ApplicationDbContext(string connectionString, string MigrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = MigrationAssembly;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
