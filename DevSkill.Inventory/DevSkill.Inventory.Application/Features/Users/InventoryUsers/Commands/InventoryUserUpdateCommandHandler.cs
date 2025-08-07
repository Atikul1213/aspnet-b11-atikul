using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands
{
    public class InventoryUserUpdateCommandHandler : IRequestHandler<InventoryUserUpdateCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public InventoryUserUpdateCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(InventoryUserUpdateCommand request, CancellationToken cancellationToken)
        {
            var inventoryUser = _mapper.Map<InventoryUser>(request);

            await _applicationUnitOfWork.InventoryUserRepository.UpdateAsync(inventoryUser);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
