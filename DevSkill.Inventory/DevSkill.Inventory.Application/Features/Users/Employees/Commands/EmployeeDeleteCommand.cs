using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Commands
{
    public class EmployeeDeleteCommand : IRequest
    {
        public EmployeeDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
