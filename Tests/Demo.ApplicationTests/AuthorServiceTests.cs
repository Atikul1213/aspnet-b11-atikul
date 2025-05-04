using Autofac.Extras.Moq;
using Demo.Application.Exceptions;
using Demo.Application.Services;
using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Repositories;
using Demo.Domain.Services;
using Moq;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Demo.ApplicationTests
{
    [ExcludeFromCodeCoverage]
    public class AuthorServiceTests
    {
        private AutoMock _moq;
        private IAuthorService _authorService;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
        private Mock<IAuthorRepository> _authorRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _authorService = _moq.Create<AuthorService>();
            _applicationUnitOfWorkMock = _moq.Mock<IApplicationUnitOfWork>();
            _authorRepositoryMock = _moq.Mock<IAuthorRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _applicationUnitOfWorkMock?.Reset();
            _authorRepositoryMock?.Reset();
        }

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

        [Test]
        public async Task AddAuthor_UniqueName_AddsAuthor()
        {
            // Arrange
            Author author = new Author()
            {
                Name = "Test Author",
                Biography = "I am a test author.",
                Rating = 3.4
            };

            _applicationUnitOfWorkMock.SetupGet(x => x.AuthorRepository).Returns(_authorRepositoryMock.Object);

            _authorRepositoryMock.Setup(x => x.IsNameDuplicate(author.Name, null)).Returns(false).Verifiable();
            _authorRepositoryMock.Setup(x => x.Add(author)).Verifiable();
            _applicationUnitOfWorkMock.Setup(x => x.Save()).Verifiable();

            // Act
            _authorService.AddAuthor(author);

            // Assert
            this.ShouldSatisfyAllConditions(
                _applicationUnitOfWorkMock.VerifyAll,
                _authorRepositoryMock.VerifyAll
            );
        }


        [Test]
        public void AddAuthor_DuplicateName_ThrowsException()
        {
            // Arrange
            Author author = new Author()
            {
                Name = "Test Author",
                Biography = "I am a test author.",
                Rating = 3.4
            };

            _applicationUnitOfWorkMock.SetupGet(x => x.AuthorRepository)
                .Returns(_authorRepositoryMock.Object);

            _authorRepositoryMock.Setup(x => x.IsNameDuplicate(author.Name, null))
                .Returns(true).Verifiable();

            // Act & Assert
            Should.Throw<DuplicateAuthorNameException>(
                () => _authorService.AddAuthor(author)
                );
        }
    }
}
