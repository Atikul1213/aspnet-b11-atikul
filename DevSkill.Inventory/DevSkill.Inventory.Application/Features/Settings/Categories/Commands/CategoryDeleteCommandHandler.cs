using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Commands
{
    public class CategoryDeleteCommandHandler : IRequestHandler<CategoryDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public CategoryDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            var category = await _applicationUnitOfWork.CategoryRepository.GetByIdAsync(request.Id);

            if (category != null)
            {
                await _applicationUnitOfWork.CategoryRepository.RemoveAsync(category);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
