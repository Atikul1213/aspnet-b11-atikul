using Demo.Domain;
using Demo.Domain.Dtos;
using Demo.Domain.Entities;
using Demo.Domain.Repositories;

namespace Demo.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext dbContext,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository) : base(dbContext)
        {
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
        }
        public IBookRepository BookRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }

        public async Task<(IList<Author> data, int total, int totalDisplay)> GetAuthorsSP(int pageIndex, int pageSize, string? order, AuthorSearchDto search)
        {
            var procedureName = "GetAuthors";

            var result = await SqlUtility.QueryWithStoredProcedureAsync<Author>(procedureName,
                new Dictionary<string, object>
                {
                    {"PageIndex", pageIndex },
                    {"PageSize", pageSize },
                    {"OrderBy", order },
                    {"RatingFrom", search.RatingFrom },
                    {"RatingTo", search.RatingTo },
                    {"Name", string.IsNullOrEmpty(search.Name) ? null : search.Name },
                    {"Biography", string.IsNullOrEmpty(search.Biography) ? null : search.Biography }
                },
                new Dictionary<string, Type>
                {
                    {"Total", typeof(int) },
                    {"TotalDisplay", typeof(int) },
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }
    }
}
