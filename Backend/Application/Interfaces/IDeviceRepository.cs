using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(int id);
    }
}
