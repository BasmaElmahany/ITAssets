using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface IEmployee
    {

        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(Guid id);

        Task<Employee> CreateAsync(Employee emp);

        Task<Employee> UpdateAsync(Guid id, EmployeeDTO empDto);
        Task DeleteAsync(Guid id);
    }
}
