using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class DataReadingService : IDataReadingService
    {
        private readonly IDataReadingRepository _dataReadingRepository;

        public DataReadingService(IDataReadingRepository dataReadingRepository)
        {
            _dataReadingRepository = dataReadingRepository;
        }
        public async Task InsertDataAsync(double value, DateTime datetime, int patientMeasureId)
        {
            try
            {
                await _dataReadingRepository.InsertDataAsync(value, datetime, patientMeasureId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed inserting data", ex);
            }
        }
    }
}
