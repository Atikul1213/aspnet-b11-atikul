using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries
{
    public class GetMobileAccountListQueryHandler : IRequestHandler<GetMobileAccountListQuery, IList<MobileAccount>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetMobileAccountListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<MobileAccount>> Handle(GetMobileAccountListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.MobileAccountRepository.GetAllAsync();
        }
        #endregion
    }
}
