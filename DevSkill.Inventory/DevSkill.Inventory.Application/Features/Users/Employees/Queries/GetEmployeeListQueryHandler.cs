using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Queries
{
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, (IList<Employee> data, int total, int totalDisplay)>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetEmployeeListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<(IList<Employee> data, int total, int totalDisplay)> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.EmployeeRepository.GetPagedEmployeeAsync(request.PageIndex, request.PageSize, request?.FormatSortExpression("Name", "Id"), request.Search);
        }
        #endregion
    }
}
