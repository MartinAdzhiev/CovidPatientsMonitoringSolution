using Application.Interfaces;
using Application.Service.Interfaces;
using Domain.Entities;

namespace Application.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository; 
        }
        public async Task<List<Device>> GetAll()
        {
            var devices = await _deviceRepository.GetAllAsync();

            return devices;
        }

        public async Task<Device?> GetById(int id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);

            if (device == null)
            {
                return null;
            }
            return device;
        }
    }
}
