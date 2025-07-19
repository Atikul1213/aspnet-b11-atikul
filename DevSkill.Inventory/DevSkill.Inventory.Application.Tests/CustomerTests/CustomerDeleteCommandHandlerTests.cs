using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Features.Customers.Commands;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.CustomerTests
{

    [ExcludeFromCodeCoverage]
    public class CustomerDeleteCommandHandlerTests
    {
        private AutoMock _moq;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<ICustomerRepository> _customerRepoMock;

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
            _customerRepoMock = _moq.Mock<ICustomerRepository>();

            _unitOfWorkMock.SetupGet(x => x.CustomerRepository).Returns(_customerRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWorkMock?.Reset();
            _customerRepoMock?.Reset();
        }

        [Test]
        public async Task DeleteCustomer_CustomerExists_RemovesCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer { Id = customerId, Name = "Test Customer" };

            _customerRepoMock.Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(customer);

            _customerRepoMock.Setup(r => r.RemoveAsync(customer))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(u => u.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CustomerDeleteCommandHandler(_unitOfWorkMock.Object);
            var command = new CustomerDeleteCommand(customerId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _customerRepoMock.Verify(r => r.GetByIdAsync(customerId), Times.Once);
            _customerRepoMock.Verify(r => r.RemoveAsync(customer), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteCustomer_CustomerDoesNotExist_DoesNothing()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            _customerRepoMock.Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync((Customer)null);

            var handler = new CustomerDeleteCommandHandler(_unitOfWorkMock.Object);
            var command = new CustomerDeleteCommand(customerId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _customerRepoMock.Verify(r => r.GetByIdAsync(customerId), Times.Once);
            _customerRepoMock.Verify(r => r.RemoveAsync(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}
