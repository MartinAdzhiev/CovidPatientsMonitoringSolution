namespace Application.Dtos.Responses
{
    public class WarningResponse
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public double CurrentMinThreshold { get; set; }
        public double CurrentMaxThreshold { get; set; }
        public PatientMeasureResponse PatientMeasureResponse { get; set; }
    }
}
