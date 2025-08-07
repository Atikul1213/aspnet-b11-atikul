using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetInventoryUserByIdQueryHandler : IRequestHandler<GetInventoryUserByIdQuery, InventoryUser>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetInventoryUserByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<InventoryUser> Handle(GetInventoryUserByIdQuery request, CancellationToken cancellationToken)
        {
            var inventoryUser = await _applicationUnitOfWork.InventoryUserRepository.GetByIdAsync(request.Id);

            return inventoryUser;
        }
        #endregion
    }
}
