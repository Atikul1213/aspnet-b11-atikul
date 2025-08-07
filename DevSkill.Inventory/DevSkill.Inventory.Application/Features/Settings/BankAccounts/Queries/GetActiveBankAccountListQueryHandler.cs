using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries
{
    public class GetActiveBankAccountListQueryHandler : IRequestHandler<GetActiveBankAccountListQuery, IList<BankAccount>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetActiveBankAccountListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<BankAccount>> Handle(GetActiveBankAccountListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.BankAccountRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
