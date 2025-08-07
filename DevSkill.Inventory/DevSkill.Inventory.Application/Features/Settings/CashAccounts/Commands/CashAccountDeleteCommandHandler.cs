using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Commands
{
    public class CashAccountDeleteCommandHandler : IRequestHandler<CashAccountDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public CashAccountDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(CashAccountDeleteCommand request, CancellationToken cancellationToken)
        {
            var cashAccount = await _applicationUnitOfWork.CashAccountRepository.GetByIdAsync(request.Id);

            if (cashAccount != null)
            {
                await _applicationUnitOfWork.CashAccountRepository.RemoveAsync(cashAccount);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
