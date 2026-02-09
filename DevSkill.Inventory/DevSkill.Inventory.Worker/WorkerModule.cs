using Autofac;
using DevSkill.Inventory.Application.Abstractions.Services;
using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Infrastructure.Repositories;
using DevSkill.Inventory.Infrastructure.Utilities;

namespace DevSkill.Inventory.Worker
{
    public class WorkerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public WorkerModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssembly", _migrationAssemblyName)
               .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailUtility>().As<IEmailUtility>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductAddCommand>().AsSelf();
            base.Load(builder);
        }
    }
}
