using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Service.Interfaces
{
    public interface IWarningService
    {
        Task<List<WarningResponse>> GetAllAsync();
        Task<SystemStatusResponse> GetSystemStatus();
        Task<bool> DeleteByIdAsync(int id);
        Task<WarningResponse?> GetWarningById(int id);
    }
}
