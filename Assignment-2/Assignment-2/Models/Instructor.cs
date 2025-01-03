using Assignment_2.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_2.Models
{
    public class Instructor
    {
        [Key]
        public Guid GuidId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address PresentAddress { get; set; }
        public Address PermanentAddress { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
