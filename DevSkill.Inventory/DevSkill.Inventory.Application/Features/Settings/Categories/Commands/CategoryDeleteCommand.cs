using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Commands
{
    public class CategoryDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
