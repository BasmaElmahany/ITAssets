using Itasset.Application.DTOs;
using Itassets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.Interfaces
{
    public interface IBrand
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand> GetByIdAsync(Guid id);

        Task<Brand> CreateAsync(Brand brand);

        Task<Brand> UpdateAsync(Guid id, BrandDTO brandDto);
        Task DeleteAsync(Guid id);



    }
}
