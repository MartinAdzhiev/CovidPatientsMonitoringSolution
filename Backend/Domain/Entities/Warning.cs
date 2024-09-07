namespace Domain.Entities
{
    public class Warning
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double CurrentMinThreshold { get; set; }
        public double CurrentMaxThreshold { get; set; }

        public int PatientMeasureId { get; set; }
        public PatientMeasure PatientMeasure { get; set; }
    }
}
