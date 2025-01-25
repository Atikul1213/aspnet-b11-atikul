using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {


        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
