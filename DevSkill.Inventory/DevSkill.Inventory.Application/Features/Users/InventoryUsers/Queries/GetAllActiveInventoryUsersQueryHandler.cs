using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetAllActiveInventoryUsersQueryHandler : IRequestHandler<GetAllActiveInventoryUsersQuery, IList<InventoryUser>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetAllActiveInventoryUsersQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<InventoryUser>> Handle(GetAllActiveInventoryUsersQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.InventoryUserRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
