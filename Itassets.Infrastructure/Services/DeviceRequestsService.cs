using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;


namespace Itassets.Infrastructure.Services
{
    public class DeviceRequestsService : IDeviceRequest
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeviceRequestsService(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DeviceRequests> CreateAsync(DeviceRequests deviceRequests)
        {
            await _context.DeviceRequests.AddAsync(deviceRequests);
            await _context.SaveChangesAsync();
            return deviceRequests;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid DeviceRequest ID.");

            var deviceRequest = await _context.DeviceRequests.FindAsync(id);
            _context.DeviceRequests.Remove(deviceRequest);

            if (deviceRequest == null)
                throw new Exception("deviceRequest not found");

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<DeviceRequests>> GetAllAsync()
        {
            var deviceRequests = await _context.DeviceRequests.Include(d=>d.category).Include(d => d.Office).Select(b => b).ToListAsync();

            return deviceRequests;
        }
        public async Task<DeviceRequests> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid deviceRequests ID.");

            var deviceRequests = await _context.DeviceRequests.Include(d => d.category).Include(d => d.Office).FirstOrDefaultAsync(b => b.Id == id);

            if (deviceRequests == null)
                throw new KeyNotFoundException("deviceRequests not found.");

            return deviceRequests;
        }

        public async Task<DeviceRequests> UpdateAsync(Guid id, DeviceRequestsDTO devReqDto)
        {
            var existingdevReq = await _context.DeviceRequests.FindAsync(id);
            if (existingdevReq == null) throw new Exception("DeviceRequest not found");
            _mapper.Map(devReqDto, existingdevReq);
            _context.Entry(existingdevReq).Property(c => c.DeviceName).IsModified = true;
            _context.Entry(existingdevReq).Property(c => c.DeviceCount).IsModified = true;
            _context.Entry(existingdevReq).Property(c => c.officeId).IsModified = true;
            _context.Entry(existingdevReq).Property(c => c.categoryID).IsModified = true;
            _context.Entry(existingdevReq).Property(c => c.Date).IsModified = true;
            await _context.SaveChangesAsync();
            return existingdevReq;

        }



    }
}
