namespace DevSkill.Inventory.Domain.Entities
{
    public class LogTable : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }
        public string? MachineName { get; set; }
        public string? ThreadId { get; set; }
    }
}
