using Data;
using Npgsql;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class PatientMeasureRepository : IPatientMeasureRepository
    {
        private readonly DbContext _dbContext;

        public PatientMeasureRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertPatientMeasureAsync(string name, string embg, string measureType, double minThreshold, double maxThreshold, int deviceId)
        {
            try
            {
                using (var conn = await _dbContext.GetOpenConnectionAsync())
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"PatientMeasures\" (\"Name\", \"Embg\", \"MeasureType\", \"MinThreshold\", \"MaxThreshold\", \"DeviceId\") VALUES (@name, @embg, @measureType, @minThreshold, @maxThreshold, @deviceId)", conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("embg", embg);
                    cmd.Parameters.AddWithValue("measureType", measureType);
                    cmd.Parameters.AddWithValue("minThreshold", minThreshold);
                    cmd.Parameters.AddWithValue("maxThreshold", maxThreshold);
                    cmd.Parameters.AddWithValue("deviceId", deviceId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed inserting PatientMeasure data", ex);
            }
        }

        public async Task<(double MinThreshold, double MaxThreshold)> GetThresholdsByPatientMeasureIdAsync(int patientMeasureId)
        {
            try
            {
                using (var conn = await _dbContext.GetOpenConnectionAsync())
                using (var cmd = new NpgsqlCommand("SELECT \"MinThreshold\", \"MaxThreshold\" FROM public.\"PatientMeasures\" WHERE \"Id\" = @patientMeasureId", conn))
                {
                    cmd.Parameters.AddWithValue("patientMeasureId", patientMeasureId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            double minThreshold = reader.GetDouble(reader.GetOrdinal("MinThreshold"));
                            double maxThreshold = reader.GetDouble(reader.GetOrdinal("MaxThreshold"));

                            return (minThreshold, maxThreshold);
                        }
                        else
                        {
                            return (default, default);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed retrieving PatientMeasure thresholds", ex);
            }
        }

        public async Task<int> GetPatientMeasureIdByEmbgAndMeasureTypeAsync(string embg, string measureType)
        {
            try
            {
                using (var conn = await _dbContext.GetOpenConnectionAsync())
                using (var cmd = new NpgsqlCommand("SELECT \"Id\" FROM public.\"PatientMeasures\" WHERE \"Embg\" = @embg AND \"MeasureType\" = @measureType LIMIT 1", conn))
                {
                    cmd.Parameters.AddWithValue("embg", embg);
                    cmd.Parameters.AddWithValue("measureType", measureType);

                    var result = await cmd.ExecuteScalarAsync();

                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed PatientMeasure sensor ID", ex);
            }
        }
    }
}
