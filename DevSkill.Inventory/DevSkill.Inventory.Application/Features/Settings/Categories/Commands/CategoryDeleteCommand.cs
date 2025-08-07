using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Commands
{
    public class CategoryDeleteCommand : IRequest
    {
        public CategoryDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
