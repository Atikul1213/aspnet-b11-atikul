using AutoMapper;
using DevSkill.Inventory.Application.Features.Customers.Commands;
using DevSkill.Inventory.Application.Features.Customers.Queries;
using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Commands;
using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.Categories.Commands;
using DevSkill.Inventory.Application.Features.Settings.Departments.Commands;
using DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Commands;
using DevSkill.Inventory.Application.Features.Settings.Units.Commands;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands;
using DevSkill.Inventory.Application.Features.Users.Employees.Commands;
using DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands;
using DevSkill.Inventory.Application.Features.Users.Suppliers.Commands;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models.BalanceTransfers;
using DevSkill.Inventory.Web.Areas.Admin.Models.BankAccounts;
using DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts;
using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using DevSkill.Inventory.Web.Areas.Admin.Models.Customers;
using DevSkill.Inventory.Web.Areas.Admin.Models.Department;
using DevSkill.Inventory.Web.Areas.Admin.Models.Employees;
using DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers;
using DevSkill.Inventory.Web.Areas.Admin.Models.MobileAccount;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Areas.Admin.Models.Supplier;
using DevSkill.Inventory.Web.Areas.Admin.Models.Unit;
using DevSkill.Inventory.Web.Areas.Admin.Models.UserRole;

namespace DevSkill.Inventory.Web
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            #region Product
            CreateMap<AddProductModel, ProductAddCommand>().ReverseMap();
            CreateMap<AddProductModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, ProductUpdateCommand>().ReverseMap();
            CreateMap<ProductSearchDto, ProductSearchModel>().ReverseMap();
            CreateMap<Product, ProductAddCommand>().ReverseMap();
            CreateMap<Product, ProductUpdateCommand>().ReverseMap();
            CreateMap<ProductSearchDto, GetProductQuery>().ReverseMap();
            #endregion

            #region Category

            CreateMap<CategoryAddCommand, AddCategoryModel>().ReverseMap();
            CreateMap<UpdateCategoryCommand, UpdateCategoryModel>().ReverseMap();
            CreateMap<Category, CategoryAddCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Category, AddCategoryModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();

            #endregion

            #region Unit

            CreateMap<UnitAddCommand, AddUnitModel>().ReverseMap();
            CreateMap<UpdateUnitCommand, UpdateUnitModel>().ReverseMap();
            CreateMap<ProductUnit, UnitAddCommand>().ReverseMap();
            CreateMap<ProductUnit, UpdateUnitCommand>().ReverseMap();
            CreateMap<ProductUnit, AddUnitModel>().ReverseMap();
            CreateMap<ProductUnit, UpdateUnitModel>().ReverseMap();
            CreateMap<ProductUnit, UnitModel>().ReverseMap();

            #endregion

            #region Department

            CreateMap<DepartmentAddCommand, AddDepartmentModel>().ReverseMap();
            CreateMap<UpdateDepartmentCommand, UpdateDepartmentModel>().ReverseMap();
            CreateMap<Department, DepartmentAddCommand>().ReverseMap();
            CreateMap<Department, UpdateDepartmentCommand>().ReverseMap();
            CreateMap<Department, AddDepartmentModel>().ReverseMap();
            CreateMap<Department, UpdateDepartmentModel>().ReverseMap();
            CreateMap<Department, DepartmentModel>().ReverseMap();

            #endregion

            #region UserRole

            CreateMap<UserRoleAddCommand, AddUserRoleModel>().ReverseMap();
            CreateMap<UpdateUserRoleCommand, UpdateUserRoleModel>().ReverseMap();
            CreateMap<UserRole, UserRoleAddCommand>().ReverseMap();
            CreateMap<UserRole, UpdateUserRoleCommand>().ReverseMap();
            CreateMap<UserRole, AddUserRoleModel>().ReverseMap();
            CreateMap<UserRole, UpdateUserRoleModel>().ReverseMap();
            CreateMap<UserRole, UserRoleModel>().ReverseMap();

            #endregion

            #region Supplier
            CreateMap<SupplierAddCommand, AddSupplierModel>().ReverseMap();
            CreateMap<SupplierUpdateCommand, UpdateSupplierModel>().ReverseMap();
            CreateMap<Supplier, SupplierAddCommand>().ReverseMap();
            CreateMap<Supplier, SupplierUpdateCommand>().ReverseMap();
            CreateMap<Supplier, AddSupplierModel>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierModel>().ReverseMap();
            CreateMap<Supplier, SupplierModel>().ReverseMap();
            #endregion

            #region Employee
            CreateMap<EmployeeAddCommand, AddEmployeeModel>().ReverseMap();
            CreateMap<EmployeeUpdateCommand, UpdateEmployeeModel>().ReverseMap();
            CreateMap<Employee, EmployeeAddCommand>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateCommand>().ReverseMap();
            CreateMap<Employee, AddEmployeeModel>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            #endregion

            #region InventoryUser
            CreateMap<InventoryUserAddCommand, AddInventoryUserModel>().ReverseMap();
            CreateMap<InventoryUserUpdateCommand, UpdateInventoryUserModel>().ReverseMap();
            CreateMap<InventoryUser, InventoryUserAddCommand>().ReverseMap();
            CreateMap<InventoryUser, InventoryUserUpdateCommand>().ReverseMap();
            CreateMap<InventoryUser, AddInventoryUserModel>().ReverseMap();
            CreateMap<InventoryUser, UpdateInventoryUserModel>().ReverseMap();
            CreateMap<InventoryUser, InventoryUserModel>().ReverseMap();
            #endregion

            #region Customer
            CreateMap<CustomerAddCommand, AddCustomerModel>().ReverseMap();
            CreateMap<CustomerUpdateCommand, UpdateCustomerModel>().ReverseMap();
            CreateMap<Customer, CustomerAddCommand>().ReverseMap();
            CreateMap<Customer, CustomerUpdateCommand>().ReverseMap();
            CreateMap<Customer, AddCustomerModel>().ReverseMap();
            CreateMap<Customer, UpdateCustomerModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<CustomerSearchDto, GetCustomerListQuery>().ReverseMap();
            #endregion

            #region CashAccount

            CreateMap<CashAccountAddCommand, AddCashAccountModel>().ReverseMap();
            CreateMap<UpdateCashAccountCommand, UpdateCashAccountModel>().ReverseMap();
            CreateMap<CashAccount, CashAccountAddCommand>().ReverseMap();
            CreateMap<CashAccount, UpdateCashAccountCommand>().ReverseMap();
            CreateMap<CashAccount, AddCashAccountModel>().ReverseMap();
            CreateMap<CashAccount, UpdateCashAccountModel>().ReverseMap();
            CreateMap<CashAccount, CashAccountModel>().ReverseMap();

            #endregion

            #region Bank Account

            CreateMap<BankAccountAddCommand, AddBankAccountModel>().ReverseMap();
            CreateMap<UpdateBankAccountCommand, UpdateBankAccountModel>().ReverseMap();
            CreateMap<BankAccount, BankAccountAddCommand>().ReverseMap();
            CreateMap<BankAccount, UpdateBankAccountCommand>().ReverseMap();
            CreateMap<BankAccount, AddBankAccountModel>().ReverseMap();
            CreateMap<BankAccount, UpdateBankAccountModel>().ReverseMap();
            CreateMap<BankAccount, BankAccountModel>().ReverseMap();

            #endregion

            #region Mobile Account

            CreateMap<MobileAccountAddCommand, AddMobileAccountModel>().ReverseMap();
            CreateMap<UpdateMobileAccountCommand, UpdateMobileAccountModel>().ReverseMap();
            CreateMap<MobileAccount, MobileAccountAddCommand>().ReverseMap();
            CreateMap<MobileAccount, UpdateMobileAccountCommand>().ReverseMap();
            CreateMap<MobileAccount, AddMobileAccountModel>().ReverseMap();
            CreateMap<MobileAccount, UpdateMobileAccountModel>().ReverseMap();
            CreateMap<MobileAccount, MobileAccountModel>().ReverseMap();

            #endregion

            #region Balance Transfter

            CreateMap<BalanceTransferAddCommand, AddBalanceTransferModel>().ReverseMap();
            CreateMap<BalanceTransfer, BalanceTransferAddCommand>().ReverseMap();
            CreateMap<BalanceTransfer, AddBalanceTransferModel>().ReverseMap();
            CreateMap<BalanceTransfer, BalanceTransferModel>().ReverseMap();

            #endregion
        }
    }
}
