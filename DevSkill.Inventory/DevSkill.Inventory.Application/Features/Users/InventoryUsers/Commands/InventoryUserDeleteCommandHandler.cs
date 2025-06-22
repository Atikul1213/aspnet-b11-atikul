using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands
{
    public class InventoryUserDeleteCommandHandler : IRequestHandler<InventoryUserDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public InventoryUserDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task Handle(InventoryUserDeleteCommand request, CancellationToken cancellationToken)
        {
            var inventoryUser = await _applicationUnitOfWork.InventoryUserRepository.GetByIdAsync(request.Id);

            if (inventoryUser is not null)
            {
                await _applicationUnitOfWork.InventoryUserRepository.RemoveAsync(inventoryUser);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
