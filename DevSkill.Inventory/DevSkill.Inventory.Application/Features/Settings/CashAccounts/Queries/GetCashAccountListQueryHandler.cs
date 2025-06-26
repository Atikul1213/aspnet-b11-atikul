using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries
{
    public class GetCashAccountListQueryHandler : IRequestHandler<GetCashAccountListQuery, IList<CashAccount>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetCashAccountListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<CashAccount>> Handle(GetCashAccountListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.CashAccountRepository.GetAllAsync();
        }
        #endregion
    }
}
