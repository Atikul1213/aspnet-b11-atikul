using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries
{
    public class GetCashAccountByIdQueryHandler : IRequestHandler<GetCashAccountByIdQuery, CashAccount>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetCashAccountByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<CashAccount> Handle(GetCashAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var cashAccount = await _applicationUnitOfWork.CashAccountRepository.GetByIdAsync(request.Id);
            return cashAccount;
        }
        #endregion
    }

}
