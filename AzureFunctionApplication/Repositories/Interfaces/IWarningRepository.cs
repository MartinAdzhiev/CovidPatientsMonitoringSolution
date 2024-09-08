namespace Repositories.Interfaces
{
    public interface IWarningRepository
    {
        Task<int> GetWarningIdAsync(int patientMeasureId);
        Task InsertWarningAsync(int patientMeasureId, DateTime time, double currentMinThreshold, double currentMaxThreshold, double value);
    }
}
