using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWarningRepository
    {
        Task<List<Warning>> GetAllAsync();
        Task<Warning?> GetByIdAsync(int id);
        Task<int> CountWarnings();
        Task<bool> DeleteByIdAsync(int id);
    }
}
