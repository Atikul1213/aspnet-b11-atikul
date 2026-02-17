using Cortex.Mediator.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQuery : DataTables, IQuery<(IList<Product>, int, int)>
    {
        public ProductSearchDto SearchItem { get; set; }
    }
}
