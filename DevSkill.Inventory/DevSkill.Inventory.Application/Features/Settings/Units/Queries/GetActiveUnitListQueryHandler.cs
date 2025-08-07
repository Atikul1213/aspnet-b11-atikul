using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Queries
{
    public class GetActiveUnitListQueryHandler : IRequestHandler<GetActiveUnitListQuery, IList<ProductUnit>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetActiveUnitListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<ProductUnit>> Handle(GetActiveUnitListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.ProductUnitRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
