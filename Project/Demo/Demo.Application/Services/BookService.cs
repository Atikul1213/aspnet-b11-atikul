using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Services;
namespace Demo.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public BookService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public void AddBook(Book book)
        {
            _applicationUnitOfWork.BookRepository.Add(book);
            _applicationUnitOfWork.Save();
        }
    }
}
