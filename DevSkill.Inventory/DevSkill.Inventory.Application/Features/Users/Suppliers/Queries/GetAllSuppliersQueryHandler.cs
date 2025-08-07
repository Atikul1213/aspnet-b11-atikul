using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Queries
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, IList<Supplier>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetAllSuppliersQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Supplier>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.SupplierRepository.GetAllAsync();
        }
        #endregion
    }
}
