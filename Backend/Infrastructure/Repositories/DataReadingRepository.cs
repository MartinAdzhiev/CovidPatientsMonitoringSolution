using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DataReadingRepository : IDataReadingRepository
    {
        private readonly AppDbContext _context;

        public DataReadingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<DataReading>> GetDataReadingsAfterDate(int patientMeasureId, DateTime dateTime)
        {
            var sql = @"
                SELECT 
                    ""PatientMeasureId"",
                    time_bucket('10 minutes', ""DateTime"") AS DateTime,
                    AVG(""Value"") AS Value
                FROM ""DataReadings""
                WHERE ""PatientMeasureId"" = @p0
                AND (""DateTime"" >= @p1 OR @p1 IS NULL)
                GROUP BY ""PatientMeasureId"", DateTime
                ORDER BY DateTime"
;

            object[] parameters = { patientMeasureId, dateTime };

            var result = await _context.DataReadings
                                      .FromSqlRaw(sql, parameters)
                                      .ToListAsync();

            return result;
        }

        public async Task<List<DataReading>> GetDataReadingsWithinInterval(int patientMeasureId, DateTime dateTimeFrom, DateTime dateTimeTo)
        {

            var sql = @"
                SELECT 
                    ""PatientMeasureId"",
                    time_bucket('10 minutes', ""DateTime"") AS DateTime,
                    AVG(""Value"") AS Value
                FROM ""DataReadings""
                WHERE ""PatientMeasureId"" = @p0
                AND (""DateTime"" >= @p1 OR @p1 IS NULL)
                AND (""DateTime"" <= @p2 OR @p2 IS NULL)
                GROUP BY ""PatientMeasureId"", DateTime
                ORDER BY DateTime";

            object[] parameters = { patientMeasureId, dateTimeFrom, dateTimeTo };

            var result = await _context.DataReadings
                                      .FromSqlRaw(sql, parameters)
                                      .ToListAsync();

            return result;
        }

        public async Task<List<DataReading>> LastReadingForAllPatientMeasures()
        {
            var lastReadings = await _context.DataReadings
                .GroupBy(dr => dr.PatientMeasureId)
                .Select(group => group.OrderByDescending(dr => dr.DateTime).FirstOrDefault())
                .ToListAsync();

            return lastReadings;
        }

        public async Task<List<DataReading>> LastTenReadingsForPatientMeasure(int patientMeasureId)
        {
            return await _context.DataReadings.Where(dr => dr.PatientMeasureId == patientMeasureId).OrderByDescending(dr => dr.DateTime).Take(10).Select(m => (DataReading?)m).ToListAsync();
        }
    }
}
