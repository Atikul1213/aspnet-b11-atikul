using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Models
{
    public class Session
    {
        [Key]
        public Guid GuidId { get; set; }
        public int DurationInHour { get; set; }
        public string LearningObjective { get; set; }

    }
}
