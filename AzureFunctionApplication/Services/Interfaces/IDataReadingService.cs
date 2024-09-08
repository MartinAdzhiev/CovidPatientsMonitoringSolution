namespace Services.Interfaces
{
    public interface IDataReadingService
    {
        Task InsertDataAsync(double value, DateTime datetime, int patientMeasureId);
    }
}
