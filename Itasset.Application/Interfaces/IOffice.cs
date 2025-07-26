using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface IOffice
    {
        Task<IEnumerable<Office>> GetAllAsync();
        Task<Office> GetByIdAsync(Guid id);

        Task<Office> CreateAsync(Office office);

        Task<Office> UpdateAsync(Guid id, OfficeDTO officeDto);
        Task DeleteAsync(Guid id);
    }
}
