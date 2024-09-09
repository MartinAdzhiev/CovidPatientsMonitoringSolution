using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _context.Devices
                .Include(d => d.PatientMeasures)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.PatientMeasures)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
