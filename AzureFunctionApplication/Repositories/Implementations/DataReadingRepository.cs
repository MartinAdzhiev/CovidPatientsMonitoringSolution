using Data;
using Npgsql;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class DataReadingRepository : IDataReadingRepository
    {
        private readonly DbContext _dbContext;
        public DataReadingRepository(DbContext context)
        {
            _dbContext = context;
        }
        public async Task InsertDataAsync(double value, DateTime datetime, int patientMeasureId)
        {
            try
            {
                using (var conn = await _dbContext.GetOpenConnectionAsync())
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"DataReadings\" (\"Value\", \"DateTime\", \"PatientMeasureId\") VALUES (@value, @datetime, @patientMeasureId)", conn))
                {
                    cmd.Parameters.AddWithValue("value", value);
                    cmd.Parameters.AddWithValue("datetime", datetime);
                    cmd.Parameters.AddWithValue("patientMeasureId", patientMeasureId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed inserting data", ex);
            }
        }
    }
}
