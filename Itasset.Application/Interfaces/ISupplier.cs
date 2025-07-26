using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface ISupplier
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier> GetByIdAsync(Guid id);

        Task<Supplier> CreateAsync(Supplier supplier);

        Task<Supplier> UpdateAsync(Guid id, SupplierDTO supplierDto);
        Task DeleteAsync(Guid id);
    }
}
