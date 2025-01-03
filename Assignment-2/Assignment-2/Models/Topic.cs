using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Models
{
    public class Topic
    {
        [Key]
        public Guid GuidId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Session> Sessions { get; set; }

    }
}
