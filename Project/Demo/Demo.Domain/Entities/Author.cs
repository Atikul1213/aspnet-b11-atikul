namespace Demo.Domain.Entities
{
    public class Author : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public double Rating { get; set; }
    }
}
