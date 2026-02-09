using Autofac.Extras.Moq;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.ProductTests
{
    [ExcludeFromCodeCoverage]
    public class ProductDeleteCommandHandlerTests
    {
        private AutoMock _moq;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<IProductRepository> _productRepoMock;

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

            _unitOfWorkMock.SetupGet(x => x.ProductRepository).Returns(_productRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWorkMock?.Reset();
            _productRepoMock?.Reset();
        }

        /**
        [Test]
        public async Task DeleteProduct_ProductExists_RemovesProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Test Product" };

            _productRepoMock.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(product);

            _productRepoMock.Setup(r => r.RemoveAsync(product))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(u => u.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ProductDeleteCommandHandler(_unitOfWorkMock.Object);
            var command = new ProductDeleteCommand(productId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepoMock.Verify(r => r.GetByIdAsync(productId), Times.Once);
            _productRepoMock.Verify(r => r.RemoveAsync(product), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteProduct_ProductDoesNotExist_DoesNothing()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepoMock.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            var handler = new ProductDeleteCommandHandler(_unitOfWorkMock.Object);
            var command = new ProductDeleteCommand(productId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepoMock.Verify(r => r.GetByIdAsync(productId), Times.Once);
            _productRepoMock.Verify(r => r.RemoveAsync(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
        }

        */
    }
}
