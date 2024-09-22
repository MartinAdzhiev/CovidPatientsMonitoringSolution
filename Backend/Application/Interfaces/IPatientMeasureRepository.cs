using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPatientMeasureRepository
    {
        Task<IEnumerable<PatientMeasure>> GetAllAsync();
        Task<PatientMeasure?> GetByIdAsync(int id);
        public Task<int> CountPatientMeasures();
        public Task UpdateAsync(PatientMeasure patientMeasure);
    }
}
