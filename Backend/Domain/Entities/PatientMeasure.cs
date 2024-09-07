﻿namespace Domain.Entities
{
    public class PatientMeasure
    {
        public int Id { get; set; }
        public string Embg {  get; set; }
        public string Name { get; set; }
        public string MeasureType { get; set; }
        public double MinThreshold { get; set; }
        public double MaxThreshold { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public ICollection<DataReading> DataReadings { get; set; }
        public ICollection<Warning> Warnings { get; set; }

    }
}
