using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Products.Query;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Queries
{
    public class GetAllProductQuery : DataTables, IRequest<(IList<Product>, int, int)>, IGetProductQuery
    {
        public ProductSearchDto SearchItem { get; set; }
    }
}
