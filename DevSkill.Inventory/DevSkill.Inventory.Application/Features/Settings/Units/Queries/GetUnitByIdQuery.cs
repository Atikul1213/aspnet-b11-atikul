using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Queries
{
    public class GetUnitByIdQuery : IRequest<ProductUnit>
    {
        public GetUnitByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
