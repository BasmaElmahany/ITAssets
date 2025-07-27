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
    public class DeviceRequestsController : ControllerBase
    {

        private readonly IDeviceRequest _devReq;
        private readonly IMapper _mapper;

        public DeviceRequestsController(IDeviceRequest devReq, IMapper mapper)
        {
            _devReq = devReq;
            _mapper = mapper;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
      Ok(await _devReq.GetAllAsync());

        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
              Ok(await _devReq.GetByIdAsync(id));

        [HttpPost("PostDeviceRequest")]
        public async Task<IActionResult> Create(DeviceRequestsDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var devReq = _mapper.Map<DeviceRequests>(dto);
            var created = await _devReq.CreateAsync(devReq);
            var result = _mapper.Map<DeviceRequestsDTO>(created);
            return Ok(new { data = result, message = "DeviceRequest Added Successfully" });
        }



        [HttpPut("PutDeviceRequest/{id}")]
        public async Task<IActionResult> Update(Guid id, DeviceRequestsDTO devReq)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _devReq.UpdateAsync(id, devReq);
            return Ok(new { data = updated, message = "DeviceRequest Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _devReq.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ DeviceRequest with ID {id} not found." });

            await _devReq.DeleteAsync(id);

            return Ok(new { message = "✅ DeviceRequest deleted successfully." });


        }
    }
}
