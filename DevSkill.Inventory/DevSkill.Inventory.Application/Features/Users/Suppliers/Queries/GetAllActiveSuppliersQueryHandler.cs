using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Queries
{
    public class GetAllActiveSuppliersQueryHandler : IRequestHandler<GetAllActiveSuppliersQuery, IList<Supplier>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetAllActiveSuppliersQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Supplier>> Handle(GetAllActiveSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.SupplierRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
