using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PatientMeasureRepository : IPatientMeasureRepository
    {
        private readonly AppDbContext _context;

        public PatientMeasureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountPatientMeasures()
        {
            return await _context.PatientMeasures.CountAsync();
        }

        public async Task<IEnumerable<PatientMeasure>> GetAllAsync()
        {
            return await _context.PatientMeasures
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PatientMeasure?> GetByIdAsync(int id)
        {
            return await _context.PatientMeasures.FindAsync(id);
        }

        public async Task UpdateAsync(PatientMeasure patientMeasure)
        {
            _context.PatientMeasures.Update(patientMeasure);
            await _context.SaveChangesAsync();
        }
    }
}
