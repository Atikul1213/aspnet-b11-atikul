using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Areas.Admin.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace DevSkill.Inventory.Web.Extensions
{
    public static class FluentValidationServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddTransient<IValidator<AddProductModel>, AddProductModelValidator>();
            services.AddTransient<IValidator<UpdateProductModel>, UpdateProductModelValidator>();

            return services;
        }
    }
}
