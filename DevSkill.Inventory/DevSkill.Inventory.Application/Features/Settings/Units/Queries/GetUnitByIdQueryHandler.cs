
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Units.Queries
{
    public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, ProductUnit>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetUnitByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<ProductUnit> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var productUnit = await _applicationUnitOfWork.ProductUnitRepository.GetByIdAsync(request.Id);
            return productUnit;
        }
        #endregion
    }
}
