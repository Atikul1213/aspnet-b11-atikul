using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Queries
{
    public class GetDepartmentListQuery : IRequest<IList<Department>>
    {
    }
}
