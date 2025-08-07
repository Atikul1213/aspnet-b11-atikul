using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands
{
    public class UserRoleDeleteCommandHandler : IRequestHandler<UserRoleDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public UserRoleDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(UserRoleDeleteCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _applicationUnitOfWork.UserRoleRepository.GetByIdAsync(request.Id);

            if (userRole != null)
            {
                await _applicationUnitOfWork.UserRoleRepository.RemoveAsync(userRole);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
