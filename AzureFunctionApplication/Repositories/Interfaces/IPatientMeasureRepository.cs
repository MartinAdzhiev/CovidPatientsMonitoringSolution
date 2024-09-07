

namespace Repositories.Interfaces
{
    public interface IPatientMeasureRepository
    {
        Task InsertPatientMeasureAsync(string name, string embg, string measureType, double minThreshold, double maxThreshold, int deviceId);
        Task<(double MinThreshold, double MaxThreshold)> GetThresholdsByPatientMeasureIdAsync(int patientMeasureId);
        Task<int> GetPatientMeasureIdByEmbgAndMeasureTypeAsync(string embg, string measureType);
    }
}
