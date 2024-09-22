using Domain.Entities;

namespace Application.Service.Interfaces
{
    public interface IDataReadingService
    {
        Task<List<DataReading>> GetDataReadingsAfterDate(int patientMeasureId, DateTime dateTime);
        Task<List<DataReading>> GetDataReadingsWithinInterval(int patientMeasureId, DateTime dateTimeFrom, DateTime dateTimeTo);
        public Task<List<DataReading>> LastReadingForAllPatientMeasures();
        public Task<List<DataReading>> LastTenReadingsForPatientMeasure(int patientMeasureId);
    }
}
