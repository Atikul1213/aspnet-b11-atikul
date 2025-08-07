using DevSkill.Inventory.Domain.Utilities;

namespace DevSkill.Inventory.Domain
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
        ISqlUtility sqlUtility { get; }
    }
}
