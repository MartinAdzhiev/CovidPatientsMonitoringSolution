namespace Services.Interfaces
{
    public interface IDeviceService
    {
        Task InsertDeviceAsync(string deviceName);
        Task<int> GetDeviceIdByNameAsync(string deviceName);
    }
}
