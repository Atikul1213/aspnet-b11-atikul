using AutoMapper;
using DevSkill.Inventory.Application.Features.ContactUs.SendMessage;
using DevSkill.Inventory.Application.Features.ContactUs.UpsertContactUsInfo;
using DevSkill.Inventory.Application.Features.Products.Commands.CreateProduct;
using DevSkill.Inventory.Application.Features.Products.Commands.UpdateProduct;
using DevSkill.Inventory.Application.Features.Products.Queries.GetProductList;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Models;

namespace DevSkill.Inventory.Web
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            #region ContactUsInfo
            CreateMap<ContactUsInfo, UpsertContactUsInfoCommand>().ReverseMap();
            CreateMap<ContactUsModel, UpsertContactUsInfoCommand>().ReverseMap();
            CreateMap<ContactUsInfo, ContactUsModel>().ReverseMap();
            CreateMap<ContactUsInfo, ContactUsInfoModel>().ReverseMap();
            CreateMap<UpsertContactUsInfoCommand, ContactUsInfoModel>().ReverseMap();
            CreateMap<SendContactUsMessageCommand, ContactUsInfoModel>().ReverseMap();

            #endregion

            #region Product
            CreateMap<AddProductModel, CreateProductCommand>().ReverseMap();
            CreateMap<AddProductModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, Product>().ReverseMap();
            CreateMap<ProductListModel, GetProductListQuery>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, UpdateProductModel>().ReverseMap();
            CreateMap<ProductSearchDto, ProductSearchModel>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            #endregion
        }
    }
}
