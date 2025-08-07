using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
    }
}
