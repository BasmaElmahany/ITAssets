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
    public class EmployeeDeviceAssignController : ControllerBase
    {
        private readonly IDeviceEmployeeAssignment _devEmpAssign;
        private readonly IMapper _mapper;

        public EmployeeDeviceAssignController(IDeviceEmployeeAssignment devEmpAssign, IMapper mapper ) {
            _devEmpAssign = devEmpAssign;   
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
        Ok(await _devEmpAssign.GetAllAsync());


        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _devEmpAssign.GetByIdAsync(id));



        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _devEmpAssign.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ Assignment with ID {id} not found." });

            await _devEmpAssign.DeleteAsync(id);

            return Ok(new { message = "✅ Assignment deleted successfully." });


        }



        [HttpPost("EmpDeviceAssignment")]
        public async Task<IActionResult> Assign(EmployeeDeviceAssignmentDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var employeeDeviceAssignmentDTO = _mapper.Map<EmployeeDeviceAssignmentDTO>(dto);
            var created = await _devEmpAssign.AssignAsync(employeeDeviceAssignmentDTO);
            var result = _mapper.Map<EmployeeDeviceAssignmentDTO>(created);
            return Ok(new { data = result, message = "EmployeeDeviceAssignment Added Successfully" });
        }


        [HttpPut("ReturnDevice")]
        public async Task<IActionResult> Return (EmployeeDeviceReturn deviceReturn)
        {
            var updated = await _devEmpAssign.ReturnAsync(deviceReturn);
            return Ok(new { data = updated, message = "Device Returned Successfully" });
        }
    }
}
