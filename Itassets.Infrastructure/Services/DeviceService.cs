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
    public class DeviceService : IDevice
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public DeviceService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            var Devices = await _context.Device.Include(d=>d.Brand).Include(d => d.Category).Include(d => d.Supplier).AsNoTracking().ToListAsync();
            return Devices;
        }

        public async Task<DevicedetailsDTO?> GetByIdAsync(Guid id)
        {
            if (id ==null)
                throw new ArgumentException("Invalid Device ID", nameof(id));

            var Device = await _context.Device
                .Include(d => d.Brand)
                .Include(d => d.Category)
                .Include(d => d.Supplier)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (Device == null)
                return null;

            return new DevicedetailsDTO
            {
                Id = Device.Id,
                Name = Device.Name,
                SerialNumber = Device.SerialNumber,
                Status = Device.Status,
                BrandName = Device.Brand.Name,
                CategoryName = Device.Category.Name,
                SupplierName = Device.Supplier?.Name,
                IsAvailable = Device.IsAvailable,
                IsFaulty = Device.IsFaulty,
                PhotoUrl = Device.PhotoUrl
            };
        }

        public async Task<Device> CreateWithPhotoAsync(CreateDeviceWithPhotoRequest request, string webRootPath)
        {
            var DeviceDto = new DeviceDTO
            {
                Name = request.Name,
                SerialNumber = request.SerialNumber,
                Status = request.Status,
                IsFaulty = request.IsFaulty,
                IsAvailable = request.IsAvailable,
                CategoryID = request.CategoryID,
                BrandId = request.BrandId,
                SupplierID = request.SupplierID,
                Qty = request.Qty,
            };

            if (request.Photo != null && request.Photo.Length > 0)
            {
                var uploadsPath = Path.Combine(webRootPath, "images", "devices");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(request.Photo.FileName);
                var fullPath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await request.Photo.CopyToAsync(stream);

                DeviceDto.PhotoUrl = $"/images/devices/{fileName}";
            }

            var device = _mapper.Map<Device>(DeviceDto);
            _context.Device.Add(device);
            await _context.SaveChangesAsync();

            return device;
        }

        public async Task<Device> UpdateWithPhotoAsync(Guid id, UpdateDeviceWithPhotoRequest request, string webRootPath)
        {
            var existingDevice = await _context.Device.FindAsync(id);
            if (existingDevice == null)
                throw new Exception("Device not found");

            // Update fields
            existingDevice.Name = request.Name;
            existingDevice.SerialNumber = request.SerialNumber;
            existingDevice.Status = request.Status;
            existingDevice.BrandId = request.BrandId;
            existingDevice.CategoryID = request.CategoryID;
            existingDevice.SupplierID = request.SupplierID;
            existingDevice.IsAvailable = request.IsAvailable;
            existingDevice.IsFaulty = request.IsFaulty;
            existingDevice.Qty = request.Qty;

            // Handle new photo
            if (request.Photo != null && request.Photo.Length > 0)
            {
                var uploadsPath = Path.Combine(webRootPath, "images", "devices");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                // Delete old photo if exists
                if (!string.IsNullOrEmpty(existingDevice.PhotoUrl))
                {
                    var oldPhotoPath = Path.Combine(webRootPath, existingDevice.PhotoUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (File.Exists(oldPhotoPath))
                        File.Delete(oldPhotoPath);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(request.Photo.FileName);
                var fullPath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await request.Photo.CopyToAsync(stream);

                existingDevice.PhotoUrl = $"/images/devices/{fileName}";
            }

            await _context.SaveChangesAsync();
            return existingDevice;
        }

        public async Task DeleteWithPhotoAsync(Guid id, string webRootPath)
        {
            var device = await _context.Device.FindAsync(id);
            if (device != null)
            {


                if (!string.IsNullOrEmpty(device.PhotoUrl))
                {
                    var photoPath = Path.Combine(webRootPath, device.PhotoUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (File.Exists(photoPath))
                        File.Delete(photoPath);
                }

                _context.Device.Remove(device);
                await _context.SaveChangesAsync();
            }
        }


    }
}
