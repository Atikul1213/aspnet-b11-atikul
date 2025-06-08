namespace DevSkill.Inventory.Domain.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
