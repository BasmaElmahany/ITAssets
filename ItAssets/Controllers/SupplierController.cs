using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItAssets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplier _supplier;
        private readonly IMapper _mapper;
        public SupplierController(ISupplier supplier, IMapper mapper)
        {
            _supplier = supplier;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _supplier.GetAllAsync());

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _supplier.GetByIdAsync(id));

        [HttpPost("PostSupplier")]
        public async Task<IActionResult> Create(SupplierDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var supplier = _mapper.Map<Supplier>(dto);
            var created = await _supplier.CreateAsync(supplier);
            var result = _mapper.Map<SupplierDTO>(created);
            return Ok(new { data = result, message = "Supplier Added Successfully" });
        }



        [HttpPut("PutSupplier/{id}")]
        public async Task<IActionResult> Update(Guid id, SupplierDTO supplier)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _supplier.UpdateAsync(id, supplier);
            return Ok(new { data = updated, message = "Supplier Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _supplier.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ Supplier with ID {id} not found." });

            await _supplier.DeleteAsync(id);

            return Ok(new { message = "✅ Supplier deleted successfully." });


        }

    }
}
