using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Queries
{
    public class GetCategoryListQuery : IRequest<IList<Category>>
    {
    }
}
