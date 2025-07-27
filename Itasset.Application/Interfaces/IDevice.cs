using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface IDevice
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<DevicedetailsDTO?> GetByIdAsync(Guid id);
        Task<Device> CreateWithPhotoAsync(CreateDeviceWithPhotoRequest request, string webRootPath);
        Task<Device> UpdateWithPhotoAsync(Guid id, UpdateDeviceWithPhotoRequest request, string webRootPath);
        Task DeleteWithPhotoAsync(Guid id, string webRootPath);
    }
}
