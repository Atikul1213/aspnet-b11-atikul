using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Commands
{
    public class UnitDeleteCommand : IRequest
    {
        public UnitDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
