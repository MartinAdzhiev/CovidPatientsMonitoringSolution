using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Service.Interfaces;
using AutoMapper;

namespace Application.Service
{
    public class PatientMeasureService : IPatientMeasureService
    {
        private readonly IPatientMeasureRepository _patientMeasureRepository;
        private readonly IMapper _mapper;

        public PatientMeasureService(IPatientMeasureRepository patientMeasureRepository, IMapper mapper)
        {
            _patientMeasureRepository = patientMeasureRepository;
            _mapper = mapper;
        }

        public async Task<bool> ChangeThreshold(int patientMeasureId, double min_threshold, double max_threshold)
        {
            var patientMeasure = await _patientMeasureRepository.GetByIdAsync(patientMeasureId);
           
            patientMeasure.MinThreshold = min_threshold;
            patientMeasure.MaxThreshold = max_threshold;
            await _patientMeasureRepository.UpdateAsync(patientMeasure);

            return true;
        }

        public async Task<IEnumerable<PatientMeasureResponse>> GetAll()
        {
            var patientMeasures = await _patientMeasureRepository.GetAllAsync();
            var responses = patientMeasures.Select(pm => _mapper.Map<PatientMeasureResponse>(pm));

            return responses;
        }

        public async Task<PatientMeasureResponse?> GetById(int id)
        {
            var patientMeasure = await _patientMeasureRepository.GetByIdAsync(id);

            if (patientMeasure == null)
            {
                return null;
            }
            return _mapper.Map<PatientMeasureResponse>(patientMeasure);
        }
    }
}
