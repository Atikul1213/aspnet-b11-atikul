using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Settings.Categories.Commands;
using DevSkill.Inventory.Application.Features.Settings.Categories.Queries;
using DevSkill.Inventory.Application.Features.Settings.Departments.Commands;
using DevSkill.Inventory.Application.Features.Settings.Departments.Queries;
using DevSkill.Inventory.Application.Features.Settings.Units.Commands;
using DevSkill.Inventory.Application.Features.Settings.Units.Queries;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries;
using DevSkill.Inventory.Application.Features.Users.Employees.Commands;
using DevSkill.Inventory.Application.Features.Users.Employees.Queries;
using DevSkill.Inventory.Application.Features.Users.Suppliers.Commands;
using DevSkill.Inventory.Application.Features.Users.Suppliers.Queries;
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
                cfg.RegisterServicesFromAssembly(typeof(GetCategoryListQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetCategoryByIdQuery).Assembly);


                cfg.RegisterServicesFromAssembly(typeof(UnitAddCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UnitDeleteCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateUnitCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUnitByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUnitListQuery).Assembly);


                cfg.RegisterServicesFromAssembly(typeof(DepartmentAddCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateDepartmentCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DepartmentDeleteCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetDepartmentListQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetDepartmentByIdQuery).Assembly);


                cfg.RegisterServicesFromAssembly(typeof(UserRoleAddCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateUserRoleCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UserRoleDeleteCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUserRoleListQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetUserRoleByIdQuery).Assembly);

                cfg.RegisterServicesFromAssembly(typeof(SupplierAddCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SupplierUpdateCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SupplierDeleteCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetSupplierListQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetSupplierByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetSupplierCountQuery).Assembly);

                cfg.RegisterServicesFromAssembly(typeof(EmployeeAddCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(EmployeeUpdateCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(EmployeeDeleteCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetEmployeeListQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetEmployeeByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetEmployeeCountQuery).Assembly);


            });

            return services;
        }
    }
}
