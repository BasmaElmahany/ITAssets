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
    public class OfficeController : ControllerBase
    {
        private readonly IOffice _office;
        private readonly IMapper _mapper;


        public OfficeController(IOffice office, IMapper mapper)
        {
            _office = office;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
             Ok(await _office.GetAllAsync());

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _office.GetByIdAsync(id));

        [HttpPost("PostOffice")]
        public async Task<IActionResult> Create(OfficeDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var office = _mapper.Map<Office>(dto);
            var created = await _office.CreateAsync(office);
            var result = _mapper.Map<OfficeDTO>(created);
            return Ok(new { data = result, message = "Office Added Successfully" });
        }



        [HttpPut("PutOffice/{id}")]
        public async Task<IActionResult> Update(Guid id, OfficeDTO office)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _office.UpdateAsync(id, office);
            return Ok(new { data = updated, message = "Office Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _office.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ Office with ID {id} not found." });

            await _office.DeleteAsync(id);

            return Ok(new { message = "✅ Office deleted successfully." });


        }
    }
}
