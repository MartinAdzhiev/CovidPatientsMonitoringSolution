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
        public async Task<List<Device>> GetAllAsync()
        {
            return await _context.Devices
                .Include(d => d.PatientMeasures)
                .ToListAsync(); 
        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }
    }
}
