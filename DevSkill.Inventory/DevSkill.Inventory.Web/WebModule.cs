using Autofac;
using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Infrastructure.Repositories;
using DevSkill.Inventory.Infrastructure.Utilities;

namespace DevSkill.Inventory.Web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public WebModule(string connectionString, string migrationAssembly)
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

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailUtility>().As<IEmailUtility>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductAddCommand>().AsSelf();

            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductUnitRepository>().As<IProductUnitRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierRepository>().As<ISupplierRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InventoryUserRepository>().As<IInventoryUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CashAccountRepository>().As<ICashAccountRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BankAccountRepository>().As<IBankAccountRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MobileAccountRepository>().As<IMobileAccountRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BalanceTransferRepository>().As<IBalanceTransferRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesRepository>().As<ISalesRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SaleProductRepository>().As<ISaleProductRepository>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
