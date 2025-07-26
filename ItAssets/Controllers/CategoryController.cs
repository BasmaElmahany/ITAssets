using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Microsoft.AspNetCore.Mvc;



namespace ItAssets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;
        private readonly IMapper _mapper;


        public CategoryController(ICategory category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
             Ok(await _category.GetAllAsync());

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _category.GetByIdAsync(id));

        [HttpPost("PostCategory")]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var category = _mapper.Map<Category>(dto);
            var created = await _category.CreateAsync(category);
            var result = _mapper.Map<CategoryDTO>(created);
            return Ok(new { data = result, message = "Category Added Successfully" });
        }



        [HttpPut("PutCategory/{id}")]
        public async Task<IActionResult> Update(Guid id, CategoryDTO category)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _category.UpdateAsync(id, category);
            return Ok(new { data = updated, message = "Category Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _category.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ Category with ID {id} not found." });

            await _category.DeleteAsync(id);

            return Ok(new { message = "✅ Category deleted successfully." });


        }
    }
}
