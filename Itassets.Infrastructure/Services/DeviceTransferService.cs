using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itassets.Infrastructure.Services
{
    public class DeviceTransferService : IDeviceTransfer
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeviceTransferService ( ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DeviceTransfer> CreateAsync(DeviceTransfer devTrans)
        {
            var empAssign = await _context.EmployeeDeviceAssignment.Where(ed => ed.DeviceID == devTrans.DeviceID && ed.EmployeeID == devTrans.OldEmpId).FirstOrDefaultAsync();
            empAssign.EmployeeID = devTrans.NewEmpId;

            await _context.DeviceTransfer.AddAsync(devTrans);
            await _context.SaveChangesAsync();
            return devTrans;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid DeviceTransfer ID.");

            var deviceTransfer = await _context.DeviceTransfer.FindAsync(id);
            _context.DeviceTransfer.Remove(deviceTransfer);

            if (deviceTransfer == null)
                throw new Exception("DeviceTransfer not found");

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DeviceTransfer>> GetAllAsync()
        {
            var DeviceTransfers = await _context.DeviceTransfer.Include(d=>d.device).Include(d=>d.emp1).Include(d=>d.emp2).Select(c => c).ToListAsync();

            return DeviceTransfers;
        }

        public async Task<DeviceTransfer> GetByIdAsync(Guid id)
        {

            if (id == Guid.Empty)
                throw new ArgumentException("Invalid DeviceTransfer ID.");

            var deviceTransfer = await _context.DeviceTransfer.FirstOrDefaultAsync(b => b.Id == id);

            if (deviceTransfer == null)
                throw new KeyNotFoundException("deviceTransfer not found.");

            return deviceTransfer;
        }

        public async Task<DeviceTransfer> UpdateAsync(Guid id, DeviceTransferDTO devTransDto)
        {
            var existingtrans = await _context.DeviceTransfer.FindAsync(id);
            if (existingtrans == null) throw new Exception("existingtrans not found");
            _mapper.Map(devTransDto, existingtrans);
            _context.Entry(existingtrans).Property(c => c.DeviceID).IsModified = true;
            _context.Entry(existingtrans).Property(c => c.OldEmpId).IsModified = true;
            _context.Entry(existingtrans).Property(c => c.NewEmpId).IsModified = true;
            _context.Entry(existingtrans).Property(c => c.DateOnly).IsModified = true;
            await _context.SaveChangesAsync();


            var empAssign = await _context.EmployeeDeviceAssignment.Where(ed => ed.DeviceID == devTransDto.DeviceID && ed.EmployeeID == devTransDto.OldEmpId).FirstOrDefaultAsync();
            empAssign.EmployeeID = devTransDto.NewEmpId;

            return existingtrans;
        }


    }
}
