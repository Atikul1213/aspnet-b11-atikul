using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Queries
{
    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, IList<Department>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetDepartmentListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Department>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.DepartmentRepository.GetAllAsync();
        }
        #endregion
    }
}
