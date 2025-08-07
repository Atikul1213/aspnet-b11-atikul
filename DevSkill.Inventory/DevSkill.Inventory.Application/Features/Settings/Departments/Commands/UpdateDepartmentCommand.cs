using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
    }
}
