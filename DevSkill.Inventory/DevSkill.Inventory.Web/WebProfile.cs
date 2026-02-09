using AutoMapper;
using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;

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
        }
    }
}
