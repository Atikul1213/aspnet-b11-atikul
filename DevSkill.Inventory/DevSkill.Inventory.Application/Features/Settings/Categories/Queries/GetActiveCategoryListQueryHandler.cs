using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Categories.Queries
{
    public class GetActiveCategoryListQueryHandler : IRequestHandler<GetActiveCategoryListQuery, IList<Category>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetActiveCategoryListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Category>> Handle(GetActiveCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.CategoryRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
