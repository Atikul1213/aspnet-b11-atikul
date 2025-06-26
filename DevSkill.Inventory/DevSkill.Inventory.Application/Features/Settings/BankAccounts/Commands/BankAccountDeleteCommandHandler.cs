using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands
{
    public class BankAccountDeleteCommandHandler : IRequestHandler<BankAccountDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public BankAccountDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(BankAccountDeleteCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _applicationUnitOfWork.BankAccountRepository.GetByIdAsync(request.Id);

            if (bankAccount != null)
            {
                await _applicationUnitOfWork.BankAccountRepository.RemoveAsync(bankAccount);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
