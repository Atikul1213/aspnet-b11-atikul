using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries
{
    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQuery, UserRole>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetUserRoleByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<UserRole> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var userRole = await _applicationUnitOfWork.UserRoleRepository.GetByIdAsync(request.Id);
            return userRole;
        }
        #endregion
    }
}
