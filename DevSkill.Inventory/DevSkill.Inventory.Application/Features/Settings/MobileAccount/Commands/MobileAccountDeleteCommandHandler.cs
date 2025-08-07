using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Commands
{
    public class MobileAccountDeleteCommandHandler : IRequestHandler<MobileAccountDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public MobileAccountDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(MobileAccountDeleteCommand request, CancellationToken cancellationToken)
        {
            var mobileAccount = await _applicationUnitOfWork.MobileAccountRepository.GetByIdAsync(request.Id);

            if (mobileAccount != null)
            {
                await _applicationUnitOfWork.MobileAccountRepository.RemoveAsync(mobileAccount);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
