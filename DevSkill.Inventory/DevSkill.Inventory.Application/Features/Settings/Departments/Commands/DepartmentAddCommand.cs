using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Commands
{
    public class DepartmentAddCommand : IRequest
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
    }
}
