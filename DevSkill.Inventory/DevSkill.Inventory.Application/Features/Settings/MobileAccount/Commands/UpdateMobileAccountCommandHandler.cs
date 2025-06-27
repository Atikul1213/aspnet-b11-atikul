using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Commands
{
    public class UpdateMobileAccountCommandHandler : IRequestHandler<UpdateMobileAccountCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UpdateMobileAccountCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(UpdateMobileAccountCommand request, CancellationToken cancellationToken)
        {
            var mobileAccount = _mapper.Map<MobileAccount>(request);

            await _applicationUnitOfWork.MobileAccountRepository.UpdateAsync(mobileAccount);
            await _applicationUnitOfWork.SaveAsync();
        }
        #endregion
    }
}
