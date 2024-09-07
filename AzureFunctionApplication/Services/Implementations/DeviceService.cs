using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Security.Cryptography;

namespace Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(IDeviceRepository deviceRepository, ILogger<DeviceService> logger)
        {
            _deviceRepository = deviceRepository;
            _logger = logger;
        }

        public async Task<int> GetDeviceIdByNameAsync(string deviceName)
        {
            return await _deviceRepository.GetDeviceIdByNameAsync(deviceName);
        }


        public async Task InsertDeviceAsync(string deviceName)
        {
            using var randomGenerator = RandomNumberGenerator.Create();

            await _deviceRepository.InsertDeviceAsync(deviceName);
        }
    }
}
