using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UpdateUserRoleCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = _mapper.Map<UserRole>(request);

            await _applicationUnitOfWork.UserRoleRepository.UpdateAsync(userRole);
            await _applicationUnitOfWork.SaveAsync();
        }
        #endregion
    }
}
