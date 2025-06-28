using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Commands
{
    public class BalanceTransferAddCommandHandler : IRequestHandler<BalanceTransferAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public BalanceTransferAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task Handle(BalanceTransferAddCommand request, CancellationToken cancellationToken)
        {
            var balanceTransfer = _mapper.Map<BalanceTransfer>(request);

            await _applicationUnitOfWork.BalanceTransferRepository.AddAsync(balanceTransfer);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
