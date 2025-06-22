using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Employees.Commands
{
    public class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public EmployeeUpdateCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);

            await _applicationUnitOfWork.EmployeeRepository.UpdateAsync(employee);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
