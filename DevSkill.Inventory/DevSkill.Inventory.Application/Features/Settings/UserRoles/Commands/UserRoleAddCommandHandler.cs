using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands
{
    public class UserRoleAddCommandHandler : IRequestHandler<UserRoleAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UserRoleAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task Handle(UserRoleAddCommand request, CancellationToken cancellationToken)
        {
            var userRole = _mapper.Map<UserRole>(request);

            await _applicationUnitOfWork.UserRoleRepository.AddAsync(userRole);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
