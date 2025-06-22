using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Queries
{
    public class GetEmployeeCountQueryHandler : IRequestHandler<GetEmployeeCountQuery, IList<Employee>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetEmployeeCountQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Employee>> Handle(GetEmployeeCountQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.EmployeeRepository.GetAllAsync();
        }
        #endregion
    }
}
