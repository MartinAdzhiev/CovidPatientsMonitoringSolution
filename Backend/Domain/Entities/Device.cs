namespace Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<PatientMeasure> PatientMeasures { get; set; }
    }
}
