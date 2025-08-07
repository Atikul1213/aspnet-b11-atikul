using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Queries
{
    public class GetSalesByIdQuery : IRequest<Sales>
    {
        public Guid Id { get; set; }
        public GetSalesByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
