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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployee employee, IMapper mapper)
        {
            _employee = employee;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
      Ok(await _employee.GetAllAsync());

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _employee.GetByIdAsync(id));

        [HttpPost("PostEmployee")]
        public async Task<IActionResult> Create(EmployeeDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var employee = _mapper.Map<Employee>(dto);
            var created = await _employee.CreateAsync(employee);
            var result = _mapper.Map<EmployeeDTO>(created);
            return Ok(new { data = result, message = "Employee Added Successfully" });
        }



        [HttpPut("PutEmployee/{id}")]
        public async Task<IActionResult> Update(Guid id, EmployeeDTO emp)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _employee.UpdateAsync(id, emp);
            return Ok(new { data = updated, message = "Employee Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _employee.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ Employee with ID {id} not found." });

            await _employee.DeleteAsync(id);

            return Ok(new { message = "✅ Employee deleted successfully." });


        }

    }
}
