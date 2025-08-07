using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Queries
{
    public class GetSalesByIdQueryHandler : IRequestHandler<GetSalesByIdQuery, Sales>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetSalesByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Sales> Handle(GetSalesByIdQuery request, CancellationToken cancellationToken)
        {
            var sales = await _applicationUnitOfWork.SalesRepository.GetByIdAsync(request.Id);

            return sales;
        }
        #endregion
    }
}
