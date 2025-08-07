using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Commands
{
    public class UpdateCashAccountCommandHandler : IRequestHandler<UpdateCashAccountCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UpdateCashAccountCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(UpdateCashAccountCommand request, CancellationToken cancellationToken)
        {
            var cashAccount = _mapper.Map<CashAccount>(request);

            await _applicationUnitOfWork.CashAccountRepository.UpdateAsync(cashAccount);
            await _applicationUnitOfWork.SaveAsync();
        }
        #endregion
    }
}
