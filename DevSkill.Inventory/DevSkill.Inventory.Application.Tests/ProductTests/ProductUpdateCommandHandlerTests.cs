using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.ProductTests
{

    [ExcludeFromCodeCoverage]
    public class ProductUpdateCommandHandlerTests
    {
        private AutoMock _moq;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<IProductRepository> _productRepoMock;
        private Mock<IMapper> _mapperMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _moq = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _moq.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = _moq.Mock<IApplicationUnitOfWork>();
            _productRepoMock = _moq.Mock<IProductRepository>();
            _mapperMock = _moq.Mock<IMapper>();

            _unitOfWorkMock.SetupGet(x => x.ProductRepository).Returns(_productRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWorkMock?.Reset();
            _productRepoMock?.Reset();
            _mapperMock?.Reset();
        }
        /**

        [Test]
        public async Task UpdateProduct_UniqueBarCode_UpdatesProduct()
        {
            // Arrange
            var command = new ProductUpdateCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Product",
                BarCode = "P-SUN001",
                CategoryId = Guid.NewGuid(),
                CategoryName = "Updated Category",
                UnitId = Guid.NewGuid(),
                BarcodeImagePath = "barcode-updated.png",
                MRPPrice = 150.00m,
                WholeSalePrice = 140.00m,
                PurchasePrice = 130.00m,
                Stock = 60,
                LowStock = 5,
                DamageStock = 3,
                ImageUrl = "image-updated.jpg"
            };

            var mappedProduct = new Product
            {
                Id = command.Id,
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

            _mapperMock.Setup(m => m.Map<Product>(command)).Returns(mappedProduct);

            _productRepoMock.Setup(repo => repo.CheckBarCodeDuplicateAsync(command.BarCode, command.Id))
                .ReturnsAsync(false);

            _productRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(uow => uow.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ProductUpdateCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<Product>(command), Times.Once);
            _productRepoMock.Verify(r => r.CheckBarCodeDuplicateAsync(command.BarCode, command.Id), Times.Once);
            _productRepoMock.Verify(r => r.UpdateAsync(It.Is<Product>(p => p.Id == command.Id)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateProduct_DuplicateBarCode_ThrowsException()
        {
            // Arrange
            var command = new ProductUpdateCommand
            {
                Id = Guid.NewGuid(),
                BarCode = "P-SUN001",
                Name = "Duplicate Product"
            };

            var product = new Product
            {
                Id = command.Id,
                BarCode = command.BarCode,
                Name = command.Name
            };

            _mapperMock.Setup(m => m.Map<Product>(command)).Returns(product);

            _productRepoMock.Setup(p => p.CheckBarCodeDuplicateAsync(command.BarCode, command.Id))
                .ReturnsAsync(true);

            var handler = new ProductUpdateCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act & Assert
            await Should.ThrowAsync<DuplicateProductBarCodeException>(() =>
                handler.Handle(command, CancellationToken.None));

            _productRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
        }
        */
    }
}