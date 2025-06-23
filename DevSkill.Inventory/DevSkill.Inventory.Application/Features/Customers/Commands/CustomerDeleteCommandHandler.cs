using DevSkill.Inventory.Domain;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Commands
{
    public class CustomerDeleteCommandHandler : IRequestHandler<CustomerDeleteCommand>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        #endregion

        #region Ctor
        public CustomerDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            var customer = await _applicationUnitOfWork.CustomerRepository.GetByIdAsync(request.Id);

            if (customer is not null)
            {
                await _applicationUnitOfWork.CustomerRepository.RemoveAsync(customer);
                await _applicationUnitOfWork.SaveAsync();
            }
        }

        #endregion
    }
}
