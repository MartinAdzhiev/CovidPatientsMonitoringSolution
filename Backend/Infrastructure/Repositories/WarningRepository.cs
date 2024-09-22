using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WarningRepository : IWarningRepository
    {
        private readonly AppDbContext _context;

        public WarningRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CountWarnings()
        {
            return await _context.Warnings.CountAsync();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var warning = await _context.Warnings.FindAsync(id);

            if (warning != null)
            {
                _context.Warnings.Remove(warning);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Warning>> GetAllAsync()
        {
            return await _context.Warnings
                .Include(w => w.PatientMeasure)
                .ToListAsync();
        }

        public async Task<Warning?> GetByIdAsync(int id)
        {
            return await _context.Warnings
                .Include(w => w.PatientMeasure)
                .FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}
