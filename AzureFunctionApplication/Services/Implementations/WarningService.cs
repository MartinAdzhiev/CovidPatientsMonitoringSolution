using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class WarningService : IWarningService
    {
        private readonly IWarningRepository _warningRepository;
        private readonly IPatientMeasureService _patientMeasureService;

        public WarningService(IWarningRepository warningRepository, IPatientMeasureService patientMeasureService)
        {
            _warningRepository = warningRepository;
            _patientMeasureService = patientMeasureService;
        }
        public async Task<bool> InsertWarningAsync(int patientMeasureId, DateTime time, double currentMinThreshold, double currentMaxThreshold, double value)
        {
            try
            {
                int existingWarning = await _warningRepository.GetWarningIdAsync(patientMeasureId);

                if (existingWarning == 0)
                {
                        await _warningRepository.InsertWarningAsync(patientMeasureId, time, currentMinThreshold, currentMaxThreshold, value);
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed inserting warning data", ex);
            }
        }

        public async Task<bool> WarningCheck(double value, int patientMeasureId)
        {
            try
            {
                var (minThreshold, maxThreshold) = await _patientMeasureService.GetThresholdsByPatientMeasureIdAsync(patientMeasureId);


                if (value >= maxThreshold || value <= minThreshold)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed checking warning conditions", ex);
            }
        }
    }
}
