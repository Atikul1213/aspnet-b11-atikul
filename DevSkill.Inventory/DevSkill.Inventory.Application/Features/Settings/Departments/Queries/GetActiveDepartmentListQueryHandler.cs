using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Queries
{
    public class GetActiveDepartmentListQueryHandler : IRequestHandler<GetActiveDepartmentListQuery, IList<Department>>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public GetActiveDepartmentListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<IList<Department>> Handle(GetActiveDepartmentListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.DepartmentRepository.GetAllWithFilterAsync(x => x.StatusId == (int)Status.Active);
        }
        #endregion
    }
}
