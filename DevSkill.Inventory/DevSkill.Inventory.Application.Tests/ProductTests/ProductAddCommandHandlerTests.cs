using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.ProductTests;

[ExcludeFromCodeCoverage]
public class ProductAddCommandHandlerTests
{
    #region Fields

    private AutoMock _moq;
    private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<IMapper> _mapperMock;

    #endregion

    #region SetUp

    [SetUp]  // This method is called before each test is run
    public void Setup()
    {
        _applicationUnitOfWorkMock = _moq.Mock<IApplicationUnitOfWork>();
        _productRepositoryMock = _moq.Mock<IProductRepository>();
        _mapperMock = _moq.Mock<IMapper>();
        _applicationUnitOfWorkMock.SetupGet(p => p.ProductRepository).Returns(_productRepositoryMock.Object);
    }

    [TearDown] // This method is called after each test is run
    public void TearDown()
    {
        _applicationUnitOfWorkMock?.Reset();
        _productRepositoryMock?.Reset();
        _mapperMock?.Reset();
    }

    [OneTimeSetUp] // This method is called once before any tests are run
    public void OneTimeSetup()
    {
        _moq = AutoMock.GetLoose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _moq.Dispose();
    }

    #endregion

    #region TestMethod

    /**
    [Test]
    public void AddProduct_UniqueBarCode_AddsProduct()
    {
        var command = new ProductAddCommand
        {
            Name = "Test Product",
            BarCode = "P-SUN000207",
            CategoryId = Guid.NewGuid(),
            CategoryName = "Category A",
            UnitId = Guid.NewGuid(),
            BarcodeImagePath = "barcode.png",
            MRPPrice = 100.00m,
            WholeSalePrice = 90.00m,
            PurchasePrice = 80.00m,
            Stock = 50,
            LowStock = 10,
            DamageStock = 5,
            ImageUrl = "image.jpg"
        };
        var productEntity = new Product
        {
            Name = command.Name,
            BarCode = command.BarCode,
            CategoryId = command.CategoryId,
            CategoryName = command.CategoryName,
            UnitId = command.UnitId,
            BarcodeImagePath = command.BarcodeImagePath,
            MRPPrice = command.MRPPrice,
            WholeSalePrice = command.WholeSalePrice,
            PurchasePrice = command.PurchasePrice,
            Stock = command.Stock,
            LowStock = command.LowStock,
            DamageStock = command.DamageStock,
            ImageUrl = command.ImageUrl
        };


        _mapperMock.Setup(m => m.Map<Product>(command)).Returns(productEntity);

        _productRepositoryMock.Setup(repo => repo.CheckBarCodeDuplicateAsync(command.BarCode, null))
            .ReturnsAsync(false);

        _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        _applicationUnitOfWorkMock.Setup(uow => uow.SaveAsync())
            .Returns(Task.CompletedTask)
            .Verifiable();

        var handler = new ProductAddCommandHandler(_applicationUnitOfWorkMock.Object, _mapperMock.Object);

        // Act
        handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<Product>(command), Times.Once);
        _productRepositoryMock.Verify(r => r.CheckBarCodeDuplicateAsync(command.BarCode, null), Times.Once);
        _productRepositoryMock.Verify(r => r.AddAsync(It.Is<Product>(p => p.BarCode == command.BarCode)), Times.Once);
        _applicationUnitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);


    }


    [Test]
    public async Task AddProduct_DuplicateBarCode_ThrowException()
    {
        // Arrange
        var command = new ProductAddCommand
        {
            Name = "Test Product",
            BarCode = "P-SUN000207",
            CategoryId = Guid.NewGuid(),
            UnitId = Guid.NewGuid(),
            MRPPrice = 100.00m,
            WholeSalePrice = 90.00m,
            PurchasePrice = 80.00m,
            Stock = 50,
            LowStock = 10,
            DamageStock = 5
        };

        var productEntity = new Product
        {
            Name = command.Name,
            BarCode = command.BarCode,
            CategoryId = command.CategoryId,
            UnitId = command.UnitId,
            MRPPrice = command.MRPPrice,
            WholeSalePrice = command.WholeSalePrice,
            PurchasePrice = command.PurchasePrice,
            Stock = command.Stock,
            LowStock = command.LowStock,
            DamageStock = command.DamageStock
        };

        _applicationUnitOfWorkMock.SetupGet(u => u.ProductRepository).Returns(_productRepositoryMock.Object);

        // Setup mapping
        _moq.Mock<IMapper>().Setup(m => m.Map<Product>(command)).Returns(productEntity);

        // Simulate barcode already exists
        _productRepositoryMock
            .Setup(p => p.CheckBarCodeDuplicateAsync(command.BarCode, null))
            .ReturnsAsync(true);

        var handler = new ProductAddCommandHandler(_applicationUnitOfWorkMock.Object, _moq.Mock<IMapper>().Object);

        // Act & Assert
        await Should.ThrowAsync<DuplicateProductBarCodeException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });

        // Also ensure AddAsync or SaveAsync is never called
        _productRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Product>()), Times.Never);
        _applicationUnitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }
    */

    #endregion
}
