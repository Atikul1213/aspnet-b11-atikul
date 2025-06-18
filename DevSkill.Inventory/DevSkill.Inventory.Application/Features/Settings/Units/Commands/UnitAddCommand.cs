using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Commands
{
    public class UnitAddCommand : IRequest
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
    }
}
