using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using DevSkill.Inventory.Web.Areas.Admin.Models.Department;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Areas.Admin.Models.Unit;
using DevSkill.Inventory.Web.Areas.Admin.Models.UserRole;
using DevSkill.Inventory.Web.Areas.Admin.Validator;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Categories;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Department;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Units;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.UserRoles;
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

            services.AddTransient<IValidator<AddCategoryModel>, AddCategoryModelValidator>();
            services.AddTransient<IValidator<UpdateCategoryModel>, UpdateCategoryModelValidator>();

            services.AddTransient<IValidator<AddUnitModel>, AddUnitModelValidator>();
            services.AddTransient<IValidator<UpdateUnitModel>, UpdateUnitModelValidator>();

            services.AddTransient<IValidator<AddDepartmentModel>, AddDepartmentModelValidator>();
            services.AddTransient<IValidator<UpdateDepartmentModel>, UpdateDepartmentModelValidator>();

            services.AddTransient<IValidator<Areas.Admin.Models.UserModel.AddUserRoleModel>, AddUserRoleModelValidator>();
            services.AddTransient<IValidator<UpdateUserRoleModel>, UpdateUserRoleModelValidator>();

            return services;
        }
    }
}
