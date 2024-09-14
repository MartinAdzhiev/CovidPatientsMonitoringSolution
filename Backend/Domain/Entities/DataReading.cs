namespace Domain.Entities
{
    public class DataReading
    {
        public double Value { get; set; }
        public DateTime DateTime { get; set; }

        public int PatientMeasureId { get; set; }
    }
}
