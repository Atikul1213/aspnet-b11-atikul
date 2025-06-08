using DevSkill.Inventory.Domain.Entities;
using System.Linq.Expressions;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TKey>
         where TEntity : class, IEntity<TKey>
         where TKey : IComparable
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Edit(TEntity entityToUpdate);
        void Update(TEntity entityToUpdate);
        Task UpdateAsync(TEntity entityToUpdate);
        Task EditAsync(TEntity entityToUpdate);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync();
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Remove(TKey id);
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
        Task RemoveAsync(TEntity entityToDelete);
        Task RemoveAsync(TKey id);
    }
}
