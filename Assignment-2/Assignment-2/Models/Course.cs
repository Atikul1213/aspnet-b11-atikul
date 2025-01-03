using Assignment_2.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Model
{
    public class Course
    {
        [Key]
        public Guid GuidId { get; set; }
        public string Title { get; set; }
        public Instructor Teacher { get; set; }
        public List<Topic> Topics { get; set; }
        public double Fees { get; set; }
        public List<AdmissionTest> Tests { get; set; }

    }
}
