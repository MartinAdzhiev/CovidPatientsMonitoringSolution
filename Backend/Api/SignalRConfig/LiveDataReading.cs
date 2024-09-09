using Newtonsoft.Json;

namespace Api.SignalRConfig
{
    public class LiveDataReading
    {
        public required string patient { get; set; }

        public required string measureType { get; set; }

        public required string embg { get; set; }
        public double? Min_threshold { get; set; }
        public double? Max_threshold { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("time")]
        public DateTime Time { get; set; }
        public int PatientMeasureId { get; set; }
        public int DeviceId { get; set; }
    }
}
