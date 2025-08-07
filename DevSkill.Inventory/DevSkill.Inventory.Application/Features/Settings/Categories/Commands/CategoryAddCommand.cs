using MediatR;
namespace DevSkill.Inventory.Application.Features.Settings.Categories.Commands
{
    public class CategoryAddCommand : IRequest
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
    }
}
