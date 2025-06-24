using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Queries
{
    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, (IList<Customer> data, int total, int totalDisplay)>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetCustomerListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<(IList<Customer> data, int total, int totalDisplay)> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            return (null, 0, 0);
        }
        #endregion
    }
}
