using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface IDeviceEmployeeAssignment
    {
        Task<IEnumerable<EmployeeDeviceAssignment>> GetAllAsync();

        Task<EmployeeDeviceAssignment> GetByIdAsync(Guid id);

        Task<EmployeeDeviceAssignmentDTO> AssignAsync(EmployeeDeviceAssignmentDTO empDevAssign);

        Task<EmployeeDeviceAssignment> ReturnAsync(EmployeeDeviceReturn empDevReturn);

        Task DeleteAsync(Guid id);



    }
}
