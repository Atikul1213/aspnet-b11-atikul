using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Queries
{
    public class GetAllBalanceTransferQueryHandler : IRequestHandler<GetAllBalanceTransferQuery, IList<BalanceTransfer>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetAllBalanceTransferQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<BalanceTransfer>> Handle(GetAllBalanceTransferQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.BalanceTransferRepository.GetAllAsync();
        }
        #endregion
    }
}
