using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Queries
{
    public class GetUnitListQueryHandler : IRequestHandler<GetUnitListQuery, IList<ProductUnit>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetUnitListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<ProductUnit>> Handle(GetUnitListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.ProductUnitRepository.GetAllAsync();
        }
        #endregion
    }
}
