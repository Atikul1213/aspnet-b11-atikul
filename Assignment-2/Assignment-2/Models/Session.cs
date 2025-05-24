namespace Assignment_2.Models
{
    public class Session : TEntity<Guid>
    {
        public Guid Id { get; set; }
        public int DurationInHour { get; set; }
        public string LearningObjective { get; set; }
    }
}
