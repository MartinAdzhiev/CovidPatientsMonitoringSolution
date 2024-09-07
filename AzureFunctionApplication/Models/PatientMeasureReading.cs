﻿using Newtonsoft.Json;

namespace Models
{
    public class PatientMeasureReading
    {
        [JsonProperty("patient")]
        public required string Name { get; set; }

        [JsonProperty("measureType")]
        public required string MeasureType { get; set; }

        [JsonProperty("embg")]
        public required string Embg { get; set; }
        public double Min_threshold { get; set; }
        public double Max_threshold { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("time")]
        public DateTime Time { get; set; }
        public int PatientMeasureId { get; set; }
        public int DeviceId { get; set; }
    }
}
