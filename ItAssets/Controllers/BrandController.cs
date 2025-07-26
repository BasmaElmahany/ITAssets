using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItAssets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrand _brand;
        private readonly IMapper _mapper;

        public BrandController(IBrand brand, IMapper mapper)
        {
            _brand = brand;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
        Ok(await _brand.GetAllAsync());

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _brand.GetByIdAsync(id));

        [HttpPost("PostBrand")]
        public async Task<IActionResult> Create(BrandDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var brand = _mapper.Map<Brand>(dto);
            var created = await _brand.CreateAsync(brand);
            var result = _mapper.Map<BrandDTO>(created);
            return Ok(new { data = result, message = "Brand Added Successfully" });
        }



        [HttpPut("PutBrand/{id}")]
        public async Task<IActionResult> Update(Guid id, BrandDTO brand)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _brand.UpdateAsync(id, brand);
            return Ok(new { data = updated, message = "Brand Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            
                var existing = await _brand.GetByIdAsync(id);
                if (existing == null)
                    return NotFound(new { message = $"❌ Brand with ID {id} not found." });

                await _brand.DeleteAsync(id);

                return Ok(new { message = "✅ Brand deleted successfully." });
            
           
        }

    }
}
