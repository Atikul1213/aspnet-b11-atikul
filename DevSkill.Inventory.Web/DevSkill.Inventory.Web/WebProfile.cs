using AutoMapper;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;

namespace DevSkill.Inventory.Web
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<AddProductModel, Product>().ReverseMap();
        }
    }
}
