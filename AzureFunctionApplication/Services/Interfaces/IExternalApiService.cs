using Models;

namespace Services.Interfaces
{
    public interface IExternalApiService
    {
        Task PostDataAsync(PatientMeasureReading patientMeasureReading);
        Task PostWarningAsync(Warning warning);
    }
}
