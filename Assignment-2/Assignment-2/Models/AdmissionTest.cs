namespace Assignment_2.Models
{
    public class AdmissionTest : TEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }

    }
}
