using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands
{
    public class InventoryUserAddCommandHandler : IRequestHandler<InventoryUserAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public InventoryUserAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(InventoryUserAddCommand request, CancellationToken cancellationToken)
        {
            var inventoryUser = _mapper.Map<InventoryUser>(request);

            await _applicationUnitOfWork.InventoryUserRepository.AddAsync(inventoryUser);
            await _applicationUnitOfWork.SaveAsync();

        }

        #endregion
    }
}
