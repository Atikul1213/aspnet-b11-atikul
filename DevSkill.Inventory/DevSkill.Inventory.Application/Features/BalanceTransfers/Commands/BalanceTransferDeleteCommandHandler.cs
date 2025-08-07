using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Commands
{
    public class BalanceTransferDeleteCommandHandler : IRequestHandler<BalanceTransferDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public BalanceTransferDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(BalanceTransferDeleteCommand request, CancellationToken cancellationToken)
        {
            var balanceTransfer = await _applicationUnitOfWork.BalanceTransferRepository.GetByIdAsync(request.Id);

            if (balanceTransfer != null)
            {
                await _applicationUnitOfWork.BalanceTransferRepository.RemoveAsync(balanceTransfer);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
