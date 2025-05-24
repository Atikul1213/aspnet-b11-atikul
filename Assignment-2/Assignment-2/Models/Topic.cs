namespace Assignment_2.Models
{
    public class Topic : TEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Session> Sessions { get; set; }

    }
}
