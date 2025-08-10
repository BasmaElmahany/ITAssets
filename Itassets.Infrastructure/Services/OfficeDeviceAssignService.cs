

using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Itassets.Infrastructure.Services
{
    public class OfficeDeviceAssignService : IDeviceOfficeAssignment
    {



        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OfficeDeviceAssignService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OfficeDeviceAssignment>> GetAllAsync()
        {
            var OfficeDeviceAssignments = await _context.OfficeDeviceAssignment.Include(e => e.Device).Include(e => e.Office).Select(b => b).ToListAsync();

            return OfficeDeviceAssignments;
        }

        public async Task<OfficeDeviceAssignment> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Assignment ID.");

            var OfficeDeviceAssignment = await _context.OfficeDeviceAssignment.FirstOrDefaultAsync(b => b.Id == id);

            if (OfficeDeviceAssignment == null)
                throw new KeyNotFoundException("OfficeDeviceAssignment not found.");

            return OfficeDeviceAssignment;
        }


        public async Task<OfficeDeviceAssignment> AssignAsync(OfficeDeviceAssignmentDTO offAssign)
        {
            if (offAssign.Qty <= 0)
                throw new ArgumentException("Assigned quantity must be greater than zero.");

            var device = await _context.Device.FindAsync(offAssign.DeviceID);
            if (device == null)
                throw new InvalidOperationException($"Device with ID {offAssign.DeviceID} not found.");

            if (device.Qty < offAssign.Qty)
                throw new InvalidOperationException($"Not enough quantity available. Available: {device.Qty}, Requested: {offAssign.Qty}");

            var offDevAss = new OfficeDeviceAssignment
            {
                DeviceID = offAssign.DeviceID,
                OfficeID = offAssign.OfficeID,
                AssignDate = offAssign.AssignDate,
                DeviceStatus = offAssign.DeviceStatus,
                Qty= offAssign.Qty

            };


            device.Qty -= offAssign.Qty;
            await _context.OfficeDeviceAssignment.AddAsync(offDevAss);
            await _context.SaveChangesAsync();
            return offDevAss;

        }
        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Assignment ID.");

            var OfficeDeviceAssignment = await _context.OfficeDeviceAssignment.FindAsync(id);
             _context.OfficeDeviceAssignment.Remove(OfficeDeviceAssignment);

            if (OfficeDeviceAssignment == null)
                throw new Exception("OfficeDeviceAssignment not found");

            await _context.SaveChangesAsync();

        }

        public async Task<OfficeDeviceAssignment> ReturnAsync(OfficeDeviceReturnDTO offDevReturn)
        {
            var offdebAssign = await _context.OfficeDeviceAssignment.FindAsync(offDevReturn.Id);

            if (offdebAssign == null)
            {
                throw new Exception("This assignment Is not available ");
            }
            offdebAssign.Id = offDevReturn.Id;
            offdebAssign.DeviceID = offdebAssign.DeviceID;
            offdebAssign.DeviceStatus = offdebAssign.DeviceStatus;
            offdebAssign.AssignDate = offdebAssign.AssignDate;
            offdebAssign.OfficeID = offdebAssign.OfficeID;
            offdebAssign.ReturnDate = offDevReturn.ReturnDate;
            offdebAssign.ReturnStatus = offDevReturn.ReturnStatus;


            var device = await _context.Device.FindAsync(offdebAssign.DeviceID);
            if (device == null)
                throw new InvalidOperationException($"Device with ID {offdebAssign.DeviceID} not found.");
            device.Qty += offdebAssign.Qty;
            await _context.SaveChangesAsync();
            return offdebAssign;
        }

    }
}
