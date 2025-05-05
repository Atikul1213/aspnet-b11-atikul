using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext context,
            IProductRepository productRepository) : base(context)
        {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; private set; }
    }
}
