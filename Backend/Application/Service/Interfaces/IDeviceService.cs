using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Service.Interfaces
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceResponse>> GetAll();
        Task<DeviceResponse?> GetById(int id);
    }
}
