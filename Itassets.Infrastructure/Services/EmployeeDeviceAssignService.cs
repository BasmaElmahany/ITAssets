

using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Itassets.Infrastructure.Services
{
    public class EmployeeDeviceAssignService : IDeviceEmployeeAssignment
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeDeviceAssignService (ApplicationDbContext context , IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDeviceAssignment>> GetAllAsync()
        {
            var EmployeeDeviceAssignments = await _context.EmployeeDeviceAssignment.Include(e=>e.Device).Include(e=>e.Employee).Select(b => b).ToListAsync();

            return EmployeeDeviceAssignments;
        }

        public async Task<EmployeeDeviceAssignment> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Assignment ID.");

            var EmployeeDeviceAssignment = await _context.EmployeeDeviceAssignment.FirstOrDefaultAsync(b => b.Id == id);

            if (EmployeeDeviceAssignment == null)
                throw new KeyNotFoundException("EmployeeDeviceAssignment not found.");

            return EmployeeDeviceAssignment;
        }


        public async Task<EmployeeDeviceAssignmentDTO> AssignAsync(EmployeeDeviceAssignmentDTO empAssign)
        {
            // Validate input
            if (empAssign == null)
                throw new ArgumentNullException(nameof(empAssign), "Assignment data cannot be null.");

            if (empAssign.Qty <= 0)
                throw new ArgumentException("Assigned quantity must be greater than zero.");

            // Fetch device
            var device = await _context.Device.FindAsync(empAssign.DeviceID);
            if (device == null)
                throw new InvalidOperationException($"Device with ID {empAssign.DeviceID} not found.");

            if (device.Qty < empAssign.Qty)
                throw new InvalidOperationException($"Not enough quantity available. Available: {device.Qty}, Requested: {empAssign.Qty}");

            // Update device quantity
            device.Qty -= empAssign.Qty;

            // Create assignment entity
            var empDevAss = new EmployeeDeviceAssignment
            {
                DeviceID = empAssign.DeviceID,
                EmployeeID = empAssign.EmployeeID,
                AssignDate = empAssign.AssignDate,
                DeviceStatus = empAssign.DeviceStatus,
                Qty = empAssign.Qty
            };

            // Add to context and save changes
            await _context.EmployeeDeviceAssignment.AddAsync(empDevAss);
            await _context.SaveChangesAsync();

            return empAssign;
        }


        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Assignment ID.");

            var EmployeeDeviceAssignment = await _context.EmployeeDeviceAssignment.FindAsync(id);
            _context.EmployeeDeviceAssignment.Remove(EmployeeDeviceAssignment);

            if (EmployeeDeviceAssignment == null)
                throw new Exception("EmployeeDeviceAssignment not found");

            await _context.SaveChangesAsync();

        }

        public async Task<EmployeeDeviceAssignment> ReturnAsync(EmployeeDeviceReturn empDevReturn)
        {
            var devEmpAssign = await _context.EmployeeDeviceAssignment.FindAsync(empDevReturn.deviceID);
            
            if (devEmpAssign == null)
            {
                throw new Exception("This assignment Is not available ");
            }
            devEmpAssign.DeviceID = devEmpAssign.DeviceID;
            devEmpAssign.DeviceStatus = devEmpAssign.DeviceStatus;
            devEmpAssign.AssignDate = devEmpAssign.AssignDate;
            devEmpAssign.EmployeeID = devEmpAssign.EmployeeID;
            devEmpAssign.ReturnDate = empDevReturn.ReturnDate;
            devEmpAssign.ReturnStatus = empDevReturn.ReturnStatus;

            var device = await _context.Device.FindAsync(devEmpAssign.DeviceID);

            if (device == null)
                throw new InvalidOperationException($"Device with ID {devEmpAssign.DeviceID} not found.");
            device.Qty += devEmpAssign.Qty;

            await  _context.SaveChangesAsync();
            return devEmpAssign; 
        }
    }
}
