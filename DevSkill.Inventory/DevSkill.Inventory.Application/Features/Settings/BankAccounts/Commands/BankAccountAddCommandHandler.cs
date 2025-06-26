using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Commands
{
    public class BankAccountAddCommandHandler : IRequestHandler<BankAccountAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public BankAccountAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task Handle(BankAccountAddCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = _mapper.Map<BankAccount>(request);

            await _applicationUnitOfWork.BankAccountRepository.AddAsync(bankAccount);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
