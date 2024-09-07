using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class PatientMeasureService : IPatientMeasureService
    {
        private readonly IPatientMeasureRepository _patientMeasureRepository;
        private readonly IDeviceService _deviceService;

        public PatientMeasureService(IPatientMeasureRepository patientMeasureRepository, IDeviceService deviceService)
        {
            _patientMeasureRepository = patientMeasureRepository;
            _deviceService = deviceService;
        }

        public async Task InsertPatientMeasureAsync(string name, string embg, string measureType, double minThreshold, double maxThreshold, string deviceName)
        {
            try
            {
                int deviceId = await _deviceService.GetDeviceIdByNameAsync(deviceName);
                await _patientMeasureRepository.InsertPatientMeasureAsync(name, embg, measureType, minThreshold, maxThreshold, deviceId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed inserting PatientMeasure", ex);
            }
        }

        public async Task<(double MinThreshold, double MaxThreshold)> GetThresholdsByPatientMeasureIdAsync(int patientMeasureId)
        {
            try
            {
                return await _patientMeasureRepository.GetThresholdsByPatientMeasureIdAsync(patientMeasureId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed retrieving PatientMeasure thresholds", ex);
            }
        }

        public async Task<int> GetPatientMeasureIdByEmbgAndMeasureTypeAsync(string embg, string measureType)
        {
            try
            {
                return await _patientMeasureRepository.GetPatientMeasureIdByEmbgAndMeasureTypeAsync(embg, measureType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed retrieving PatientMeasure ID", ex);
            }
        }
    }
}
