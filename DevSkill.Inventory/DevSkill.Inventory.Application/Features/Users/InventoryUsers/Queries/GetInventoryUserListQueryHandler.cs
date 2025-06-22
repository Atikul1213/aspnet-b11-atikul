using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries
{
    public class GetInventoryUserListQueryHandler : IRequestHandler<GetInventoryUserListQuery, (IList<InventoryUser> data, int total, int totalDisplay)>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetInventoryUserListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<(IList<InventoryUser> data, int total, int totalDisplay)> Handle(GetInventoryUserListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.InventoryUserRepository.GetPagedInventoryUserAsync(request.PageIndex, request.PageSize, request?.FormatSortExpression("EmployeeName", "Id"), request.Search);
        }
        #endregion
    }
}
