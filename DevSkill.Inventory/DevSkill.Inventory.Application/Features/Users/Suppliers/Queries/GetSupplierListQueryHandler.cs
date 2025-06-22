using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Queries
{
    public class GetSupplierListQueryHandler : IRequestHandler<GetSupplierListQuery, (IList<Supplier> data, int total, int totalDisplay)>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetSupplierListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<(IList<Supplier> data, int total, int totalDisplay)> Handle(GetSupplierListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.SupplierRepository.GetPagedSupplierAsync(request.PageIndex, request.PageSize, request?.FormatSortExpression("Name", "Id"), request.Search);
        }
        #endregion
    }
}
