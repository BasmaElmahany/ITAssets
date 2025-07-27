using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItAssets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeDeviceAssignController : ControllerBase
    {
        private readonly IDeviceOfficeAssignment _devOffAssign;
        private readonly IMapper _mapper;

        public OfficeDeviceAssignController(IDeviceOfficeAssignment devOffAssign, IMapper mapper)
        {
            _devOffAssign = devOffAssign;
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
          Ok(await _devOffAssign.GetAllAsync());


        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _devOffAssign.GetByIdAsync(id));



        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _devOffAssign.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ Assignment with ID {id} not found." });

            await _devOffAssign.DeleteAsync(id);

            return Ok(new { message = "✅ Assignment deleted successfully." });


        }



        [HttpPost("OffDeviceAssignment")]
        public async Task<IActionResult> Assign(OfficeDeviceAssignmentDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var officeDeviceAssignmentDTO = _mapper.Map<OfficeDeviceAssignmentDTO>(dto);
            var created = await _devOffAssign.AssignAsync(officeDeviceAssignmentDTO);
            var result = _mapper.Map<OfficeDeviceAssignmentDTO>(created);
            return Ok(new { data = result, message = "OfficeDeviceAssignment Added Successfully" });
        }


        [HttpPut("ReturnDevice")]
        public async Task<IActionResult> Return(OfficeDeviceReturnDTO deviceReturn)
        {
            var updated = await _devOffAssign.ReturnAsync(deviceReturn);
            return Ok(new { data = updated, message = "Device Returned Successfully" });
        }
    }
}
