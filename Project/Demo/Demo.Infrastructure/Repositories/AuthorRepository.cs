using Demo.Domain.Entities;
using Demo.Domain.Repositories;

namespace Demo.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author, Guid>, IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
