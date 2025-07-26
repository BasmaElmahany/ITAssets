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
    public class SupplierService : ISupplier
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SupplierService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            await _context.Supplier.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid supplier ID.");

            var supplier = await _context.Supplier.FindAsync(id);
            _context.Supplier.Remove(supplier);

            if (supplier == null)
                throw new Exception("supplier not found");

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            var suppliers = await _context.Supplier.Select(b => b).ToListAsync();

            return suppliers;
        }
        public async Task<Supplier> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid supplier ID.");

            var supplier = await _context.Supplier.FirstOrDefaultAsync(b => b.Id == id);

            if (supplier == null)
                throw new KeyNotFoundException("supplier not found.");

            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Guid id, SupplierDTO supplierDto)
        {
            var existingSupplier = await _context.Supplier.FindAsync(id);
            if (existingSupplier == null) throw new Exception("Supplier not found");
            _mapper.Map(supplierDto, existingSupplier);
            _context.Entry(existingSupplier).Property(c => c.Name).IsModified = true;
            _context.Entry(existingSupplier).Property(c => c.PhoneNumber).IsModified = true;
            await _context.SaveChangesAsync();
            return existingSupplier;

        }

    }
}
