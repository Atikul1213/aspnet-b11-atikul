using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Products.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevSkill.Inventory.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMediator(this IServiceCollection services, Assembly migrationAssembly)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(migrationAssembly);
                cfg.RegisterServicesFromAssembly(typeof(ProductAddCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ProductUpdateCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(ProductDeleteCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetProductQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllProductQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetProductListQuery).Assembly);
            });

            return services;
        }
    }
}
