namespace Api.SignalRConfig
{
    public class LiveWarning
    {
        public DateTime DateTime { get; set; }
        public double CurrentMinThreshold { get; set; }
        public double CurrentMaxThreshold { get; set; }
        public double Value { get; set; }
        public int PatientMeasureId { get; set; }
    }
}
