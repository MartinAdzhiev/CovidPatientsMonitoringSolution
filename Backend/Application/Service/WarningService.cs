using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Service.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class WarningService : IWarningService
    {
        private readonly IWarningRepository _warningRepository;
        private readonly IPatientMeasureRepository _patientMeasureRepository;
        private readonly IMapper _mapper;

        public WarningService(IWarningRepository warningRepository, IPatientMeasureRepository patientMeasureRepository, IMapper mapper)
        {
            _warningRepository = warningRepository;
            _patientMeasureRepository = patientMeasureRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _warningRepository.DeleteByIdAsync(id);
        }

        public async Task<List<WarningResponse>> GetAllAsync()
        {
            var warnings = await _warningRepository.GetAllAsync();
            return _mapper.Map<List<WarningResponse>>(warnings);
        }

        public async Task<SystemStatusResponse> GetSystemStatus()
        {
            var totalPatientMeasures = await _patientMeasureRepository.CountPatientMeasures();
            var totalWarnings = await _warningRepository.CountWarnings();

            var systemStatus = new SystemStatusResponse
            {
                totalPatientMeasures = totalPatientMeasures,
                totalWarnings = totalWarnings
            };

            return systemStatus;
        }

        public async Task<WarningResponse?> GetWarningById(int id)
        {
            var warning = await _warningRepository.GetByIdAsync(id);

            return _mapper.Map<WarningResponse?>(warning);
        }
    }
}
