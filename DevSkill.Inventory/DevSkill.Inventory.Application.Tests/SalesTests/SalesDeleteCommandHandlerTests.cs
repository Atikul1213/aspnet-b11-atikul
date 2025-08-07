using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Features.SalesProduct.Commands;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.SalesTests
{
    [ExcludeFromCodeCoverage]
    public class SalesDeleteCommandHandlerTests
    {
        private AutoMock _moq;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<ISalesRepository> _salesRepoMock;

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
            _salesRepoMock = _moq.Mock<ISalesRepository>();

            _unitOfWorkMock.SetupGet(x => x.SalesRepository).Returns(_salesRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWorkMock?.Reset();
            _salesRepoMock?.Reset();
        }

        [Test]
        public async Task DeleteSales_SalesExists_RemovesSales()
        {
            // Arrange
            var salesId = Guid.NewGuid();
            var sales = new Sales { Id = salesId, InvoiceNo = "INV-1003" };

            _salesRepoMock.Setup(r => r.GetByIdAsync(salesId))
                .ReturnsAsync(sales);

            _salesRepoMock.Setup(r => r.RemoveAsync(sales))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(u => u.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new SalesDeleteCommandHandler(_unitOfWorkMock.Object);
            var command = new SalesDeleteCommand(salesId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _salesRepoMock.Verify(r => r.GetByIdAsync(salesId), Times.Once);
            _salesRepoMock.Verify(r => r.RemoveAsync(sales), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteSales_SalesDoesNotExist_DoesNothing()
        {
            // Arrange
            var salesId = Guid.NewGuid();

            _salesRepoMock.Setup(r => r.GetByIdAsync(salesId))
                .ReturnsAsync((Sales)null);

            var handler = new SalesDeleteCommandHandler(_unitOfWorkMock.Object);
            var command = new SalesDeleteCommand(salesId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _salesRepoMock.Verify(r => r.GetByIdAsync(salesId), Times.Once);
            _salesRepoMock.Verify(r => r.RemoveAsync(It.IsAny<Sales>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}
