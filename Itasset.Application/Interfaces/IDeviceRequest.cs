using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface IDeviceRequest
    {
        Task<IEnumerable<DeviceRequests>> GetAllAsync();
        Task<DeviceRequests> GetByIdAsync(Guid id);

        Task<DeviceRequests> CreateAsync(DeviceRequests devReq);

        Task<DeviceRequests> UpdateAsync(Guid id, DeviceRequestsDTO DRDto);
        Task DeleteAsync(Guid id);
    }
}
