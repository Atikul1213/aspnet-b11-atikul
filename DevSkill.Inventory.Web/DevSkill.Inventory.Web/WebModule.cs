using Autofac;
using DevSkill.Inventory.Infrastructure;

namespace DevSkill.Inventory.Web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public WebModule(string connectionString, string migrationAssemble)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssemble;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
