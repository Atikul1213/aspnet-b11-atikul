using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Commands
{
    public class DepartmentDeleteCommand : IRequest
    {
        public DepartmentDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
