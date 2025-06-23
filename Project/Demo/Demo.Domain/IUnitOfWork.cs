using Demo.Domain.Utilities;

namespace Demo.Domain
{
    public interface IUnitOfWork
    {
        ISqlUtility SqlUtility { get; }
        void Save();
        Task SaveAsync();
    }
}
