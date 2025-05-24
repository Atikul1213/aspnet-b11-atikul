namespace Assignment_2.Models
{
    public class Phone : TEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; }
    }
}
