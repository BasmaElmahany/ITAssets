

using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Itassets.Infrastructure.Services
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid brand ID.");

            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);

            if (category == null)
                throw new Exception("category not found");

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var Categories = await _context.Category.Select(c=>c).ToListAsync();

            return Categories;
        }
        public async Task<Category> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Category ID.");

            var category = await _context.Category.FirstOrDefaultAsync(b => b.Id == id);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return category;
        }

        public async Task<Category> UpdateAsync(Guid id, CategoryDTO categoryDto)
        {
            var existingcategory = await _context.Category.FindAsync(id);
            if (existingcategory == null) throw new Exception("Category not found");
            _mapper.Map(categoryDto, existingcategory);
            _context.Entry(existingcategory).Property(c => c.Name).IsModified = true;
            await _context.SaveChangesAsync();
            return existingcategory;

        }
    }
}
