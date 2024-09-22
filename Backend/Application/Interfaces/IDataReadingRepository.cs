using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDataReadingRepository
    {
        Task<List<DataReading>> GetDataReadingsAfterDate(int patientMeasureId, DateTime dateTime);
        Task<List<DataReading>> GetDataReadingsWithinInterval(int patientMeasureId, DateTime dateTimeFrom, DateTime dateTimeTo);
        Task<List<DataReading>> LastTenReadingsForPatientMeasure(int patientMeasureId);
        Task<List<DataReading>> LastReadingForAllPatientMeasures();
    }
}
