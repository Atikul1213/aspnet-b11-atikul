using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Queries
{
    public class GetSupplierCountQueryHandler : IRequestHandler<GetSupplierCountQuery, IList<Supplier>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetSupplierCountQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Supplier>> Handle(GetSupplierCountQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.SupplierRepository.GetAllAsync();
        }
        #endregion
    }
}
