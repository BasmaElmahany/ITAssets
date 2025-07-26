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
    public class OfficeService : IOffice
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OfficeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Office> CreateAsync(Office office)
        {
            await _context.Office.AddAsync(office);
            await _context.SaveChangesAsync();
            return office;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid office ID.");

            var office = await _context.Office.FindAsync(id);
            _context.Office.Remove(office);

            if (office == null)
                throw new Exception("office not found");

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Office>> GetAllAsync()
        {
            var Offices = await _context.Office.Select(c => c).ToListAsync();

            return Offices;
        }
        public async Task<Office> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Office ID.");

            var Office = await _context.Office.FirstOrDefaultAsync(b => b.Id == id);

            if (Office == null)
                throw new KeyNotFoundException("Office not found.");

            return Office;
        }

        public async Task<Office> UpdateAsync(Guid id, OfficeDTO officeDto)
        {
            var existingOffice = await _context.Office.FindAsync(id);
            if (existingOffice == null) throw new Exception("Office not found");
            _mapper.Map(officeDto, existingOffice);
            _context.Entry(existingOffice).Property(c => c.Name).IsModified = true;
         
            await _context.SaveChangesAsync();
            return existingOffice;

        }
    }
}
