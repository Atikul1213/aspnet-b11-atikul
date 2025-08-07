using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Queries
{
    public class GetBalanceTransferByIdQueryHandler : IRequestHandler<GetBalanceTransferByIdQuery, BalanceTransfer>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetBalanceTransferByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<BalanceTransfer> Handle(GetBalanceTransferByIdQuery request, CancellationToken cancellationToken)
        {
            var balanceTransfer = await _applicationUnitOfWork.BalanceTransferRepository.GetByIdAsync(request.Id);
            return balanceTransfer;
        }
        #endregion
    }

}
