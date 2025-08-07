using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Queries
{
    public class CategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, IList<Category>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public CategoryListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Category>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.CategoryRepository.GetAllAsync();
        }
        #endregion
    }
}
