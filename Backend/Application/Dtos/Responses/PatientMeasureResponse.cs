namespace Application.Dtos.Responses
{
    public class PatientMeasureResponse
    {
        public int Id { get; set; }
        public string Embg { get; set; }
        public string Name { get; set; }
        public string MeasureType { get; set; }
        public double MinThreshold { get; set; }
        public double MaxThreshold { get; set; }
    }
}
