using Autofac;
using Demo.Infrastructure;

namespace Demo.Api
{
    public class ApiModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public ApiModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
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
