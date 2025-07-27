using Itasset.Application.DTOs;
using Itassets.Domain.Entities;


namespace Itasset.Application.Interfaces
{
    public interface IDeviceOfficeAssignment
    {
        Task<IEnumerable<OfficeDeviceAssignment>> GetAllAsync();

        Task<OfficeDeviceAssignment> GetByIdAsync(Guid id);

        Task<OfficeDeviceAssignment> AssignAsync(OfficeDeviceAssignmentDTO offDevAssign);

        Task<OfficeDeviceAssignment> ReturnAsync(OfficeDeviceReturnDTO effDevReturn);

        Task DeleteAsync(Guid id);
    }
}
