using DevSkill.Inventory.Web.Areas.Admin.Models.BankAccounts;
using DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts;
using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using DevSkill.Inventory.Web.Areas.Admin.Models.Customers;
using DevSkill.Inventory.Web.Areas.Admin.Models.Department;
using DevSkill.Inventory.Web.Areas.Admin.Models.Employees;
using DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Areas.Admin.Models.Supplier;
using DevSkill.Inventory.Web.Areas.Admin.Models.Unit;
using DevSkill.Inventory.Web.Areas.Admin.Models.UserRole;
using DevSkill.Inventory.Web.Areas.Admin.Validator;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Customers;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.BankAccounts;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.CashAccounts;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Categories;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Department;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Units;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.UserRoles;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Users.Employees;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Users.InventoryUsers;
using DevSkill.Inventory.Web.Areas.Admin.Validator.Users.Suppilers;
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

            services.AddTransient<IValidator<AddSupplierModel>, AddSupplierModelValidator>();
            services.AddTransient<IValidator<UpdateSupplierModel>, UpdateSupplierModelValidator>();

            services.AddTransient<IValidator<AddEmployeeModel>, AddEmployeeModelValidator>();
            services.AddTransient<IValidator<UpdateEmployeeModel>, UpdateEmployeeModelValidator>();

            services.AddTransient<IValidator<AddInventoryUserModel>, AddInventoryUserModelValidator>();
            services.AddTransient<IValidator<UpdateInventoryUserModel>, UpdateInventoryUserModelValidator>();


            services.AddTransient<IValidator<AddCustomerModel>, AddCustomerModelValidator>();
            services.AddTransient<IValidator<UpdateCustomerModel>, UpdateCustomerModelValidator>();

            services.AddTransient<IValidator<AddCashAccountModel>, AddCashAccountModelValidator>();
            services.AddTransient<IValidator<UpdateCashAccountModel>, UpdateCashAccountModelValidator>();

            services.AddTransient<IValidator<AddBankAccountModel>, AddBankAccountModelValidator>();
            services.AddTransient<IValidator<UpdateBankAccountModel>, UpdateBankAccountModelValidator>();

            return services;
        }
    }
}
