using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class DataReading
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime DateTime { get; set; }

        public int PatientMeasureId { get; set; }
        [JsonIgnore]
        public PatientMeasure PatientMeasure { get; set; }
    }
}
