using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Queries
{
    public class GetAllEmployeesQuery : IRequest<IList<Employee>>
    {
    }
}
