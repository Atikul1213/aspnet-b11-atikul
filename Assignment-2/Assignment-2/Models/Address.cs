namespace Assignment_2.Models
{
    public class Address : TEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
