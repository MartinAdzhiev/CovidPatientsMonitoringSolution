namespace Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<int> GetDeviceIdByNameAsync(string deviceName);
        Task InsertDeviceAsync(string deviceName);
    }
}
