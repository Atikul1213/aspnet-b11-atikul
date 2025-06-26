using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries
{
    public class GetBankAccountListQueryHandler : IRequestHandler<GetBankAccountListQuery, IList<BankAccount>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetBankAccountListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<BankAccount>> Handle(GetBankAccountListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.BankAccountRepository.GetAllAsync();
        }
        #endregion
    }
}
