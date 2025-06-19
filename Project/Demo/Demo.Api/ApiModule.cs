using Autofac;
using Demo.Domain;
using Demo.Domain.Repositories;
using Demo.Infrastructure;
using Demo.Infrastructure.Identity;
using Demo.Infrastructure.Repositories;

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

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
              .InstancePerLifetimeScope();

            builder.RegisterType<BookRepository>().As<IBookRepository>()
               .InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
