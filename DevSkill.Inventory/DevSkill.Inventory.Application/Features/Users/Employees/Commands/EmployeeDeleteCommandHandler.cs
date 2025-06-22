using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Commands
{
    public class EmployeeDeleteCommandHandler : IRequestHandler<EmployeeDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public EmployeeDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            var employee = await _applicationUnitOfWork.EmployeeRepository.GetByIdAsync(request.Id);

            if (employee is not null)
            {
                await _applicationUnitOfWork.EmployeeRepository.RemoveAsync(employee);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
