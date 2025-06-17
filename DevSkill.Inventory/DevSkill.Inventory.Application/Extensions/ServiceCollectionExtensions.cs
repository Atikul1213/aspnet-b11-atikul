using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Settings.Categories.Commands;
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
                cfg.RegisterServicesFromAssembly(typeof(CategoryAddCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateCategoryCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CategoryDeleteCommand).Assembly);

            });

            return services;
        }
    }
}
