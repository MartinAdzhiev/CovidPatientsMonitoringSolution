using Data;
using Npgsql;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class WarningRepository : IWarningRepository
    {
        private readonly DbContext _dbContext;

        public WarningRepository(DbContext context)
        {
            _dbContext = context;
        }
        public async Task<int> GetWarningIdAsync(int patientMeasureId)
        {
            try
            {
                using (var conn = await _dbContext.GetOpenConnectionAsync())
                using (var cmd = new NpgsqlCommand("SELECT \"Id\" FROM public.\"Warnings\" WHERE \"PatientMeasureId\" = @patientMeasureId", conn))
                {
                    cmd.Parameters.AddWithValue("patientMeasureId", patientMeasureId);

                    var result = await cmd.ExecuteScalarAsync();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed retrieving warning ID", ex);
            }
        }

        public async Task InsertWarningAsync(int patientMeasureId, DateTime time, double currentMinThreshold, double currentMaxThreshold, double value)
        {
            try
            {
                using (var conn = await _dbContext.GetOpenConnectionAsync())
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Warnings\" (\"DateTime\", \"CurrentMinThreshold\", \"CurrentMaxThreshold\", \"PatientMeasureId\", \"Value\") VALUES (@time, @currentMinThreshold, @currentMaxThreshold, @patientMeasureId, @value)", conn))
                {
                    cmd.Parameters.AddWithValue("time", time);
                    cmd.Parameters.AddWithValue("currentMinThreshold", currentMinThreshold);
                    cmd.Parameters.AddWithValue("currentMaxThreshold", currentMaxThreshold);
                    cmd.Parameters.AddWithValue("patientMeasureId", patientMeasureId);
                    cmd.Parameters.AddWithValue("value", value);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed inserting warning data", ex);
            }
        }
    }
}
