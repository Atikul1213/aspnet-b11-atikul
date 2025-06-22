using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Commands
{
    public class SupplierDeleteCommandHandler : IRequestHandler<SupplierDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public SupplierDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task Handle(SupplierDeleteCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _applicationUnitOfWork.SupplierRepository.GetByIdAsync(request.Id);

            if (supplier is not null)
            {
                await _applicationUnitOfWork.SupplierRepository.RemoveAsync(supplier);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
