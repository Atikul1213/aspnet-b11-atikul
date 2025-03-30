using Demo.Application.Exceptions;
using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Services;

namespace Demo.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    public AuthorService(IApplicationUnitOfWork applicationUnitOfWork)
    {
        _applicationUnitOfWork = applicationUnitOfWork;
    }
    public void AddAuthor(Author author)
    {
        if (!_applicationUnitOfWork.AuthorRepository.IsNameDuplicate(author.Name))
        {
            _applicationUnitOfWork.AuthorRepository.Add(author);
            _applicationUnitOfWork.Save();
        }
        else
            throw new DuplicateAuthorNameException();
    }

    public void DeleteAuthor(Author author)
    {
        _applicationUnitOfWork.AuthorRepository.Remove(author.Id);
        _applicationUnitOfWork.Save();
    }

    public Author GetAuthorById(Guid id)
    {
        return _applicationUnitOfWork.AuthorRepository.GetById(id);
    }

    public (IList<Author> data, int total, int totalDisplay) GetAuthors(int pageIndex, int pageSize, string? order, DataTablesSearch search)
    {
        return _applicationUnitOfWork.AuthorRepository.GetPagedAuthors(pageIndex, pageSize, order, search);
    }

    public void UpdateAuthor(Author author)
    {
        _applicationUnitOfWork.AuthorRepository.Update(author);
        _applicationUnitOfWork.Save();
    }
}
