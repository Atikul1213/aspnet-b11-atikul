using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetInventoryUserByEmailQueryHandler : IRequestHandler<GetInventoryUserByEmailQuery, InventoryUser>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetInventoryUserByEmailQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<InventoryUser> Handle(GetInventoryUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var inventoryUser = await _applicationUnitOfWork.InventoryUserRepository.GetAllWithFilterAsync(x => x.Email == request.Email);

            if (inventoryUser == null || !inventoryUser.Any())
            {
                return null;
            }

            return inventoryUser.FirstOrDefault();
        }
        #endregion
    }
}
