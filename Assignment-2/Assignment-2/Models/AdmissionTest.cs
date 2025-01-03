using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Models
{
    public class AdmissionTest
    {
        [Key]
        public Guid GuidId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }

    }
}
