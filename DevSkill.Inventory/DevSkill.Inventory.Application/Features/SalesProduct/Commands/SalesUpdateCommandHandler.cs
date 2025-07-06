using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Commands
{
    public class SalesUpdateCommandHandler : IRequestHandler<SalesUpdateCommand>
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
        public async Task Handle(SalesUpdateCommand request, CancellationToken cancellationToken)
        {
            var sales = _mapper.Map<Sales>(request);

            await _applicationUnitOfWork.SalesRepository.UpdateAsync(sales);
            await _applicationUnitOfWork.SaveAsync();

        }

        #endregion
    }
}
