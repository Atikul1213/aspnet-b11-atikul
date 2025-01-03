using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Models
{
    public class Phone
    {
        [Key]
        public Guid GuidId { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; }
    }
}
