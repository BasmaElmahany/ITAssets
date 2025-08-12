

using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Itassets.Infrastructure.Services
{
    public class DeviceMaintainanceScheduleService : IDeviceMaintainanceSchedule
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeviceMaintainanceScheduleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DeviceMaintainanceSchedule> CreateAsync(DeviceMaintainanceSchedule deviceMaintainanceSchedule)
        {
            await _context.DeviceMaintainanceSchedule.AddAsync(deviceMaintainanceSchedule);
            await _context.SaveChangesAsync();
            return deviceMaintainanceSchedule;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid DeviceMaintainanceSchedule ID.");

            var deviceMaintainanceSchedule = await _context.DeviceMaintainanceSchedule.FindAsync(id);
            _context.DeviceMaintainanceSchedule.Remove(deviceMaintainanceSchedule);

            if (deviceMaintainanceSchedule == null)
                throw new Exception("deviceMaintainanceSchedule not found");

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<DeviceMaintainanceSchedule>> GetAllAsync()
        {
            var DeviceMaintainanceSchedules = await _context.DeviceMaintainanceSchedule.Include(c=>c.Device).Select(c => c).ToListAsync();

            return DeviceMaintainanceSchedules;
        }
        public async Task<DeviceMaintainanceSchedule> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid DeviceMaintainanceSchedule ID.");

            var DeviceMaintainanceSchedule = await _context.DeviceMaintainanceSchedule.FirstOrDefaultAsync(b => b.Id == id);

            if (DeviceMaintainanceSchedule == null)
                throw new KeyNotFoundException("DeviceMaintainanceSchedules not found.");

            return DeviceMaintainanceSchedule;
        }

        public async Task<DeviceMaintainanceSchedule> UpdateAsync(Guid id, DeviceMaintainanceScheduleDTO DeviceMaintainanceScheduleDto)
        {
            var existingDeviceMaintainanceSchedule = await _context.DeviceMaintainanceSchedule.FindAsync(id);
            if (existingDeviceMaintainanceSchedule == null) throw new Exception("DeviceMaintainanceSchedule not found");
            _mapper.Map(DeviceMaintainanceScheduleDto, existingDeviceMaintainanceSchedule);
            _context.Entry(existingDeviceMaintainanceSchedule).Property(c => c.DeviceID).IsModified = true;

            _context.Entry(existingDeviceMaintainanceSchedule).Property(c => c.Date).IsModified = true;
            _context.Entry(existingDeviceMaintainanceSchedule).Property(c => c.Description).IsModified = true;
            _context.Entry(existingDeviceMaintainanceSchedule).Property(c => c.IsComplete).IsModified = true;

            await _context.SaveChangesAsync();
            return existingDeviceMaintainanceSchedule;

        }

    }
}
