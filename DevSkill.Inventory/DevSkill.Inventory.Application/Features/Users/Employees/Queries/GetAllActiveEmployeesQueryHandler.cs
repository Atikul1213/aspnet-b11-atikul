using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Queries
{
    public class GetAllActiveEmployeesQueryHandler : IRequestHandler<GetAllActiveEmployeesQuery, IList<Employee>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetAllActiveEmployeesQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Employee>> Handle(GetAllActiveEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.EmployeeRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
