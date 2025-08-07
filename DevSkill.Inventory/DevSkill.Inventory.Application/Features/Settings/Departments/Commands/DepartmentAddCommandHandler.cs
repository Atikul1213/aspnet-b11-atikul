using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.Departments.Commands
{
    public class DepartmentAddCommandHandler : IRequestHandler<DepartmentAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public DepartmentAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task Handle(DepartmentAddCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);
            department.CreateOnUtc = DateTime.UtcNow;

            await _applicationUnitOfWork.DepartmentRepository.AddAsync(department);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
