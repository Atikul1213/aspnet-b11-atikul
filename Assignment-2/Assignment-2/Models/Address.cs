using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Models
{
    public class Address
    {
        [Key]
        public Guid GuidId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
