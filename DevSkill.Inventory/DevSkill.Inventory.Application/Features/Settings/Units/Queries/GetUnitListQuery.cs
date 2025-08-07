using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Queries
{
    public class GetUnitListQuery : IRequest<IList<ProductUnit>>
    {
    }
}
