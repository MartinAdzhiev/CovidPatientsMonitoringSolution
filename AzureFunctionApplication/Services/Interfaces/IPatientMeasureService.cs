namespace Services.Interfaces
{
    public interface IPatientMeasureService
    {
        Task InsertPatientMeasureAsync(string name, string embg, string measureType, double minThreshold, double maxThreshold, string deviceName);
        Task<int> GetPatientMeasureIdByEmbgAndMeasureTypeAsync(string embg, string measureType);
        Task<(double MinThreshold, double MaxThreshold)> GetThresholdsByPatientMeasureIdAsync(int patientMeasureId);
    }
}
