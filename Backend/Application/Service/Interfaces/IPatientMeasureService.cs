using Application.Dtos.Responses;

namespace Application.Service.Interfaces
{
    public interface IPatientMeasureService
    {
        Task<IEnumerable<PatientMeasureResponse>> GetAll();
        Task<PatientMeasureResponse?> GetById(int id);
        Task<bool> ChangeThreshold(int patientMeasureId, double min_threshold, double max_threshold);
    }
}
