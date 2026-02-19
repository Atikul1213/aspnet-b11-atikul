using Cortex.Mediator.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.Products.Queries.GetAllProductList
{
    public class GetProductListSPQuery : DataTables, IQuery<(IList<Product>, int, int)>
    {
        public ProductSearchDto SearchItem { get; set; }
    }
}
