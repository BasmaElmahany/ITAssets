using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itassets.Infrastructure.Services
{

 
    public class BrandService : IBrand
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BrandService (ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Brand> CreateAsync(Brand brand)
        {
            await _context.Brand.AddAsync(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid brand ID.");

            var brand = await _context.Brand.FindAsync(id);
             _context.Brand.Remove(brand);

            if (brand == null)
                throw new Exception("brand not found");

            await _context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            var brands = await _context.Brand.Select(b => b).ToListAsync();

            return brands;
        }
        public async Task<Brand> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid brand ID.");

            var brand = await _context.Brand.FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null)
                throw new KeyNotFoundException("Brand not found.");

            return brand;
        }

        public async Task<Brand> UpdateAsync(Guid id, BrandDTO brandDto)
        {
            var existingbrand = await _context.Brand.FindAsync(id);
            if (existingbrand == null) throw new Exception("Brand not found");
            _mapper.Map(brandDto, existingbrand);
            _context.Entry(existingbrand).Property(c => c.Name).IsModified = true;
            await _context.SaveChangesAsync();
            return existingbrand;

        }
    }
}
