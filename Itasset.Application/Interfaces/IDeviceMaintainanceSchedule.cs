

using Itasset.Application.DTOs;
using Itassets.Domain.Entities;

namespace Itasset.Application.Interfaces
{
    public interface IDeviceMaintainanceSchedule
    {
        Task<IEnumerable<DeviceMaintainanceSchedule>> GetAllAsync();
        Task<DeviceMaintainanceSchedule> GetByIdAsync(Guid id);

        Task<DeviceMaintainanceSchedule> CreateAsync(DeviceMaintainanceSchedule deviceMaintainanceSchedule);

        Task<DeviceMaintainanceSchedule> UpdateAsync(Guid id, DeviceMaintainanceScheduleDTO Dto);
        Task DeleteAsync(Guid id);
    }
}
