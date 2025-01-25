namespace Demo.Domain.Entities
{
    public class Author : IEntity<Guid>
    {
        public Guid Id { get; set; }

    }
}
