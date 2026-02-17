using Cortex.Mediator.Queries;
using DevSkill.Core.Application;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IQuery<ResultResponse<Product>>
    {
        public Guid Id { get; set; }
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
