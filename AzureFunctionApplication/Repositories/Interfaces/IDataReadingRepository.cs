namespace Repositories.Interfaces
{
    public interface IDataReadingRepository
    {
        Task InsertDataAsync(double value, DateTime datetime, int patientMeasureId);
    }
}
