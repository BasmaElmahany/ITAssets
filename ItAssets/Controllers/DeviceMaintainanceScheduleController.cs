using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Itassets.Domain.Entities;
using Itassets.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;


namespace ItAssets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceMaintainanceScheduleController : ControllerBase
    {
        private readonly IDeviceMaintainanceSchedule _devMaintainance;
        private readonly IMapper _mapper;
        public DeviceMaintainanceScheduleController(IDeviceMaintainanceSchedule devMaintainance, IMapper mapper)
        {
            _devMaintainance = devMaintainance;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
      Ok(await _devMaintainance.GetAllAsync());

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _devMaintainance.GetByIdAsync(id));

        [HttpPost("PostMaintainance")]
        public async Task<IActionResult> Create(DeviceMaintainanceScheduleDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var devSech = _mapper.Map<DeviceMaintainanceSchedule>(dto);
            var created = await _devMaintainance.CreateAsync(devSech);
            var result = _mapper.Map<DeviceMaintainanceScheduleDTO>(created);
            return Ok(new { data = result, message = "DeviceMaintainanceSchedule Added Successfully" });
        }



        [HttpPut("PutSchedule/{id}")]
        public async Task<IActionResult> Update(Guid id, DeviceMaintainanceScheduleDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _devMaintainance.UpdateAsync(id, dto);
            return Ok(new { data = updated, message = "DeviceMaintainanceSchedule Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _devMaintainance.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ DeviceMaintainanceSchedule with ID {id} not found." });

            await _devMaintainance.DeleteAsync(id);

            return Ok(new { message = "✅ DeviceMaintainanceSchedule deleted successfully." });


        }
    }
}
