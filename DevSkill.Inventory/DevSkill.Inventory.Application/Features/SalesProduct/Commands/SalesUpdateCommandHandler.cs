using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Commands
{
    public class SalesUpdateCommandHandler : IRequestHandler<SalesUpdateCommand, Sales>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public SalesUpdateCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Sales> Handle(SalesUpdateCommand request, CancellationToken cancellationToken)
        {
            var sales = _mapper.Map<Sales>(request);

            var result = await _applicationUnitOfWork.SalesRepository.UpdateSalesAsync(sales);
            await _applicationUnitOfWork.SaveAsync();
            return result;
        }

        #endregion
    }
}
