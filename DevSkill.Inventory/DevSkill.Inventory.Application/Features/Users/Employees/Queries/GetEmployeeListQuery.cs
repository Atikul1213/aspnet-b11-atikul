using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Users.Employees.Queries;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Queries
{
    public class GetEmployeeListQuery : DataTables, IRequest<(IList<Employee> data, int total, int totalDisplay)>, IGetEmployeeQuery
    {
    }
}
