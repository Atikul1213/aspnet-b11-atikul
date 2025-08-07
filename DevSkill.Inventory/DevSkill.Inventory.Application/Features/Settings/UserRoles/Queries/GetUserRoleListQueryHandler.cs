using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries
{
    public class GetUserRoleListQueryHandler : IRequestHandler<GetUserRoleListQuery, IList<UserRole>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetUserRoleListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<UserRole>> Handle(GetUserRoleListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.UserRoleRepository.GetAllAsync();
        }
        #endregion
    }
}
