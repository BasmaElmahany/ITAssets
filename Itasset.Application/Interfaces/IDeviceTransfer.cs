using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface IDeviceTransfer
    {
        Task<IEnumerable<DeviceTransfer>> GetAllAsync();

        Task<DeviceTransfer> GetByIdAsync(Guid id);

        Task<DeviceTransfer> CreateAsync(DeviceTransfer devTrans);

        Task<DeviceTransfer> UpdateAsync(Guid id, DeviceTransferDTO devTransDto);

        Task DeleteAsync(Guid id);
    }
}
