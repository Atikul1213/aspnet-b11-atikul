using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries
{
    public class GetActiveMobileAccountListQueryHandler : IRequestHandler<GetActiveMobileAccountListQuery, IList<MobileAccount>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetActiveMobileAccountListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<MobileAccount>> Handle(GetActiveMobileAccountListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.MobileAccountRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
