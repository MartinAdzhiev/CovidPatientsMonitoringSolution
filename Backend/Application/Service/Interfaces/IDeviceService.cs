using Domain.Entities;

namespace Application.Service.Interfaces
{
    public interface IDeviceService
    {
        Task<List<Device>> GetAll();
        Task<Device?> GetById(int id);
    }
}
