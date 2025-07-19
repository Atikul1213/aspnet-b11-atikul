using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Inventory.Application.Features.Customers.Commands;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.CustomerTests
{
    [ExcludeFromCodeCoverage]
    public class CustomerAddCommandHandlerTests
    {
        private AutoMock _moq;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<ICustomerRepository> _customerRepoMock;
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
            _customerRepoMock = _moq.Mock<ICustomerRepository>();
            _mapperMock = _moq.Mock<IMapper>();

            _unitOfWorkMock.SetupGet(x => x.CustomerRepository).Returns(_customerRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWorkMock?.Reset();
            _customerRepoMock?.Reset();
            _mapperMock?.Reset();
        }

        [Test]
        public async Task AddCustomer_ValidInput_AddsCustomer()
        {
            // Arrange
            var command = new CustomerAddCommand
            {
                Name = "John Doe",
                CompanyName = "Doe Inc.",
                MobileNumber = "0123456789",
                Address = "123 Main Street",
                Email = "john@example.com",
                OpeningBalance = 500.00m,
                CurrentBalance = 500.00m,
                ImageUrl = "image.jpg",
                StatusId = 1
            };

            var customer = new Customer
            {
                Name = command.Name,
                CompanyName = command.CompanyName,
                MobileNumber = command.MobileNumber,
                Address = command.Address,
                Email = command.Email,
                OpeningBalance = command.OpeningBalance,
                CurrentBalance = command.CurrentBalance,
                ImageUrl = command.ImageUrl,
                StatusId = command.StatusId
            };

            _mapperMock.Setup(m => m.Map<Customer>(command)).Returns(customer);
            _customerRepoMock.Setup(r => r.AddAsync(customer)).Returns(Task.CompletedTask).Verifiable();
            _unitOfWorkMock.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            var handler = new CustomerAddCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<Customer>(command), Times.Once);
            _customerRepoMock.Verify(r => r.AddAsync(It.Is<Customer>(c => c.Name == command.Name)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
