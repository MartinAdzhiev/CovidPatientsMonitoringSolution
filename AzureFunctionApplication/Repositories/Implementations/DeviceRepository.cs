using Data;
using Repositories.Interfaces;
using Npgsql;

namespace Repositories.Implementations
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DbContext _dbContext;

        public DeviceRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetDeviceIdByNameAsync(string deviceName)
        {
            using (var conn = await _dbContext.GetOpenConnectionAsync())
            using (var cmd = new NpgsqlCommand("SELECT \"Id\" FROM public.\"Devices\" WHERE \"Name\" = @deviceName", conn))
            {
                cmd.Parameters.AddWithValue("deviceName", deviceName);
                var existingDeviceId = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(existingDeviceId);
            }
        }


        public async Task InsertDeviceAsync(string deviceName)
        {
            int deviceId = await GetDeviceIdByNameAsync(deviceName);

            if (deviceId == 0)
            {
                using (var conn = await _dbContext.GetOpenConnectionAsync())
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Devices\" (\"Name\") VALUES (@deviceName)", conn))
                {
                    cmd.Parameters.AddWithValue("deviceName", deviceName);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
