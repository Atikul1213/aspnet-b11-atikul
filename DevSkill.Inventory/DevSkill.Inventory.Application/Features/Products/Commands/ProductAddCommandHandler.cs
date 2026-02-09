namespace DevSkill.Inventory.Application.Features.Products.Commands
{
    //public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand>
    //{
    //    #region Fields
    //    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    //    private readonly IMapper _mapper;
    //    #endregion

    //    #region Ctor
    //    public ProductAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
    //    {
    //        _applicationUnitOfWork = applicationUnitOfWork;
    //        _mapper = mapper;
    //    }
    //    #endregion

    //    #region Methods
    //    public async Task Handle(ProductAddCommand request, CancellationToken cancellationToken)
    //    {
    //        var product = _mapper.Map<Product>(request);

    //        bool isDuplicate = await _applicationUnitOfWork.ProductRepository.CheckBarCodeDuplicateAsync(product.BarCode);

    //        if (!isDuplicate)
    //        {
    //            await _applicationUnitOfWork.ProductRepository.AddAsync(product);
    //            await _applicationUnitOfWork.SaveAsync();
    //        }
    //        else
    //        {
    //            throw new DuplicateProductBarCodeException();
    //        }
    //    }

    //    #endregion
    //}
}
