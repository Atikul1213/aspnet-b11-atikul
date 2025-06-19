using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Commands
{
    public class DepartmentDeleteCommandHandler : IRequestHandler<DepartmentDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public DepartmentDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods

        public async Task Handle(DepartmentDeleteCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationUnitOfWork.DepartmentRepository.GetByIdAsync(request.Id);

            if (department != null)
            {
                await _applicationUnitOfWork.DepartmentRepository.RemoveAsync(department);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
