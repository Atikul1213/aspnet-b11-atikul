using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Commands
{
    public class SupplierAddCommandHandler : IRequestHandler<SupplierAddCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public SupplierAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task Handle(SupplierAddCommand request, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Supplier>(request);

            await _applicationUnitOfWork.SupplierRepository.AddAsync(supplier);
            await _applicationUnitOfWork.SaveAsync();

        }

        #endregion
    }
}
