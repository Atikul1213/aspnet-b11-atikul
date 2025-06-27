using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries
{
    public class GetMobileAccountByIdQueryHandler : IRequestHandler<GetMobileAccountByIdQuery, MobileAccount>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetMobileAccountByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<MobileAccount> Handle(GetMobileAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var mobileAccount = await _applicationUnitOfWork.MobileAccountRepository.GetByIdAsync(request.Id);
            return mobileAccount;
        }
        #endregion
    }

}
