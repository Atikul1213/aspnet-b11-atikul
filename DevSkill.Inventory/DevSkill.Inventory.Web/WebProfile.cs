using AutoMapper;
using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Application.Features.Settings.Categories.Commands;
using DevSkill.Inventory.Application.Features.Settings.Departments.Commands;
using DevSkill.Inventory.Application.Features.Settings.Units.Commands;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands;
using DevSkill.Inventory.Application.Features.Users.Suppliers.Commands;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using DevSkill.Inventory.Web.Areas.Admin.Models.Department;
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
            CreateMap<AddProductModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, Product>().ReverseMap();
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
        }
    }
}
