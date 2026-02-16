using AutoMapper;
using DevSkill.Inventory.Application.Features.Products.Commands.CreateProduct;
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
            CreateMap<AddProductModel, CreateProductCommand>().ReverseMap();
            CreateMap<AddProductModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, Product>().ReverseMap();
            CreateMap<ProductSearchDto, ProductSearchModel>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            #endregion
        }
    }
}
