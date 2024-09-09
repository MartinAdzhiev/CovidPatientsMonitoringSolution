using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Service.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository; 
            _mapper = mapper;
        }
        public async Task<IEnumerable<DeviceResponse>> GetAll()
        {
            var devices = await _deviceRepository.GetAllAsync();
            var responses = devices.Select(d => _mapper.Map<DeviceResponse>(d));

            return responses;
        }

        public async Task<DeviceResponse?> GetById(int id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);

            if (device == null)
            {
                return null;
            }
            return _mapper.Map<DeviceResponse>(device);
        }
    }
}
