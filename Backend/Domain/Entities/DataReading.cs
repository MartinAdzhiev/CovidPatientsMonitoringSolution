namespace Domain.Entities
{
    public class DataReading
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime DateTime { get; set; }

        public int PatientMeasureId { get; set; }
        public PatientMeasure PatientMeasure { get; set; }
    }
}
