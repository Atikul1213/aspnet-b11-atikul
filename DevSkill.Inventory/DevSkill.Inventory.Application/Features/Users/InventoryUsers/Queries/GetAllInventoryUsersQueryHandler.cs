using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetAllInventoryUsersQueryHandler : IRequestHandler<GetAllInventoryUsersQuery, IList<InventoryUser>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetAllInventoryUsersQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<InventoryUser>> Handle(GetAllInventoryUsersQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.InventoryUserRepository.GetAllAsync();
        }
        #endregion
    }
}
