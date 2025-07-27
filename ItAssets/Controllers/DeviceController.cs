using AutoMapper;
using Itasset.Application.DTOs;
using Itasset.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItAssets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IDevice _Idevice;
        private readonly IMapper _imapper;

        public DeviceController(IDevice device, IMapper mapper, IWebHostEnvironment env) {
            _env = env;
            _Idevice = device;
            _imapper = mapper;

        }

        [HttpGet("GetAllDevices")]
        public async Task<IActionResult> GetAll() =>
     Ok(await _Idevice.GetAllAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == null)
                return BadRequest("Invalid Device ID");

            var result = await _Idevice.GetByIdAsync(id);

            if (result == null)
                return NotFound("Device not found");

            return Ok(result);
        }

        [HttpPost("create-with-photo")]
        public async Task<IActionResult> CreateWithPhoto([FromForm] CreateDeviceWithPhotoRequest request)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");
            var webRootPath = _env.WebRootPath;
            var createdDevice = await _Idevice.CreateWithPhotoAsync(request, webRootPath);
            var result = _imapper.Map<DeviceDTO>(createdDevice);

            return Ok(new
            {
                message = "Device Created Successfully",
                data = result
            });
        }
        [HttpPut("{id}/update-with-photo")]
        public async Task<IActionResult> UpdateWithPhoto(Guid id, [FromForm] UpdateDeviceWithPhotoRequest request)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");
            var updatedDevice = await _Idevice.UpdateWithPhotoAsync(id, request, _env.WebRootPath);
            var result = _imapper.Map<DeviceDTO>(updatedDevice);

            return Ok(new { message = "Device Updated Successfully", data = result });
        }

        [HttpDelete("{id}/delete-with-photo")]
        public async Task<IActionResult> DeleteWithPhoto(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");
            await _Idevice.DeleteWithPhotoAsync(id, _env.WebRootPath);
            return Ok(new { message = "Device Deleted Successfully" });
        }


    }
}
