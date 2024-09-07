using Npgsql;

namespace Data
{
    public class DbContext
    {
        private readonly string _databaseConnectionString;

        public DbContext(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }

        public async Task<NpgsqlConnection> GetOpenConnectionAsync()
        {
            var conn = new NpgsqlConnection(_databaseConnectionString);
            await conn.OpenAsync();
            return conn;
        }

    }
}
