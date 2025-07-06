using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Queries
{
    public class GetActiveCustomerListQueryHandler : IRequestHandler<GetActiveCustomerListQuery, IList<Customer>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetActiveCustomerListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Customer>> Handle(GetActiveCustomerListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.CustomerRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
