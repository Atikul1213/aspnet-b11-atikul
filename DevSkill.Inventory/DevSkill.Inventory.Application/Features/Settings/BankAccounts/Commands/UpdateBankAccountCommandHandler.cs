using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands
{
    public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UpdateBankAccountCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = _mapper.Map<BankAccount>(request);

            await _applicationUnitOfWork.BankAccountRepository.UpdateAsync(bankAccount);
            await _applicationUnitOfWork.SaveAsync();
        }
        #endregion
    }
}
