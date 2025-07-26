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
    public class EmployeeService : IEmployee
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Employee> CreateAsync(Employee emp)
        {
            await _context.Employee.AddAsync(emp);
            await _context.SaveChangesAsync();
            return emp;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Employee ID.");

            var emp = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(emp);

            if (emp == null)
                throw new Exception("Employee not found");

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _context.Employee.Select(b => b).ToListAsync();

            return employees;
        }
        public async Task<Employee> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid employee ID.");

            var employee = await _context.Employee.FirstOrDefaultAsync(b => b.Id == id);

            if (employee == null)
                throw new KeyNotFoundException("employee not found.");

            return employee;
        }

        public async Task<Employee> UpdateAsync(Guid id, EmployeeDTO employeeDto)
        {
            var existingemployee = await _context.Employee.FindAsync(id);
            if (existingemployee == null) throw new Exception("employee not found");
            _mapper.Map(employeeDto, existingemployee);
            _context.Entry(existingemployee).Property(c => c.Name).IsModified = true;
            _context.Entry(existingemployee).Property(c => c.Position).IsModified = true;
            _context.Entry(existingemployee).Property(c => c.Email).IsModified = true;
            await _context.SaveChangesAsync();
            return existingemployee;

        }
    }
}
