using Autofac.Extras.Moq;
using AutoMapper;
using DevSkill.Inventory.Application.Features.SalesProduct.Commands;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests.SalesTests
{

    [ExcludeFromCodeCoverage]
    public class SalesAddCommandHandlerTests
    {
        private AutoMock _moq;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<ISalesRepository> _salesRepoMock;
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
            _salesRepoMock = _moq.Mock<ISalesRepository>();
            _mapperMock = _moq.Mock<IMapper>();

            _unitOfWorkMock.SetupGet(x => x.SalesRepository).Returns(_salesRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWorkMock?.Reset();
            _salesRepoMock?.Reset();
            _mapperMock?.Reset();
        }

        [Test]
        public async Task AddSales_ValidCommand_ReturnsSales()
        {
            // Arrange
            var command = new SalesAddCommand
            {
                InvoiceNo = "INV-1001",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerPhoneNumber = "0123456789",
                StatusId = 1,
                SalesTypeId = 2,
                Vat = 5.00m,
                NetAmount = 100.00m,
                Discount = 10.00m,
                TotalAmount = 95.00m,
                PaidAmount = 80.00m,
                DueAmount = 15.00m,
                AccountTypeId = 1,
                AccountNoId = Guid.NewGuid(),
                Note = "Sample Note",
                TermsAndConditions = "Sample T&C"
            };

            var mappedSales = new Sales
            {
                InvoiceNo = command.InvoiceNo,
                SaleDate = command.SaleDate,
                CustomerId = command.CustomerId,
                CustomerName = command.CustomerName,
                CustomerPhoneNumber = command.CustomerPhoneNumber,
                StatusId = command.StatusId,
                SalesTypeId = command.SalesTypeId,
                Vat = command.Vat,
                NetAmount = command.NetAmount,
                Discount = command.Discount,
                TotalAmount = command.TotalAmount,
                PaidAmount = command.PaidAmount,
                DueAmount = command.DueAmount,
                AccountTypeId = command.AccountTypeId,
                AccountNoId = command.AccountNoId,
                Note = command.Note,
                TermsAndConditions = command.TermsAndConditions
            };

            var savedSales = new Sales
            {
                Id = Guid.NewGuid(),
                InvoiceNo = mappedSales.InvoiceNo,
            };

            _mapperMock.Setup(m => m.Map<Sales>(command)).Returns(mappedSales);

            _salesRepoMock.Setup(r => r.InsertSalesAsync(mappedSales))
                .ReturnsAsync(savedSales)
                .Verifiable();

            _unitOfWorkMock.Setup(u => u.SaveAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new SalesAddCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<Sales>(command), Times.Once);
            _salesRepoMock.Verify(r => r.InsertSalesAsync(mappedSales), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(savedSales.Id));
            Assert.That(result.InvoiceNo, Is.EqualTo(command.InvoiceNo));
        }
    }
}
