namespace Services.Interfaces
{
    public interface IWarningService
    {
        Task<bool> InsertWarningAsync(int patientMeasureId, DateTime time, double currentMinThreshold, double currentMaxThreshold, double value);
        Task<bool> WarningCheck(double value, int patientMeasureId);
    }
}
