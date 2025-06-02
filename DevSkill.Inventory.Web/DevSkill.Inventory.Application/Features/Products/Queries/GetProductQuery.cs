using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Products.Query;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Queries
{
    public class GetProductQuery : DataTables, IRequest<(IList<Product>, int, int)>, IGetProductQuery
    {
        public string? Name { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string? Sku { get; set; }
    }
}
