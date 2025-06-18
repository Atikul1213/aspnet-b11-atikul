using AutoMapper;
using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Application.Features.Settings.Categories.Commands;
using DevSkill.Inventory.Application.Features.Settings.Units.Commands;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Areas.Admin.Models.Unit;

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


        }
    }
}
