using Application.Interfaces;
using Application.Service.Interfaces;
using Domain.Entities;

namespace Application.Service
{
    public class DataReadingService : IDataReadingService
    {
        private readonly IDataReadingRepository _dataReadingRepository;

        public DataReadingService(IDataReadingRepository dataReadingRepository)
        {
            _dataReadingRepository = dataReadingRepository;
        }
        public async Task<List<DataReading>> GetDataReadingsAfterDate(int patientMeasureId, DateTime dateTime)
        {
            return await _dataReadingRepository.GetDataReadingsAfterDate(patientMeasureId, dateTime);
        }

        public async Task<List<DataReading>> GetDataReadingsWithinInterval(int patientMeasureId, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return await _dataReadingRepository.GetDataReadingsWithinInterval(patientMeasureId, dateTimeFrom, dateTimeTo);
        }

        public async Task<List<DataReading>> LastReadingForAllPatientMeasures()
        {
            return await _dataReadingRepository.LastReadingForAllPatientMeasures();
        }

        public async Task<List<DataReading>> LastTenReadingsForPatientMeasure(int patientMeasureId)
        {
            return await _dataReadingRepository.LastTenReadingsForPatientMeasure(patientMeasureId);
        }
    }
}
