using Autofac;
using Demo.Application.Features.Books.Commands;
using Demo.Application.Services;
using Demo.Domain;
using Demo.Domain.Repositories;
using Demo.Domain.Services;
using Demo.Infrastructure;
using Demo.Infrastructure.Repositories;
using Demo.Infrastructure.Services;

namespace Demo.Web
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

            builder.RegisterType<BookRepository>().As<IBookRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookService>().As<IBookService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthorService>().As<IAuthorService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookAddCommand>().AsSelf();

            base.Load(builder);
        }
    }
}
