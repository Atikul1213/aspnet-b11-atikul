using AutoMapper;
using DevSkill.Inventory.Application.Features.ContactUs.UpsertContactUsInfo;
using DevSkill.Inventory.Application.Features.Products.Commands.CreateProduct;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Api
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            #region ContactUsInfo
            CreateMap<ContactUsInfo, UpsertContactUsInfoCommand>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            #endregion
        }
    }
}
