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
    public class DeviceTransferController : ControllerBase
    {
        private readonly IDeviceTransfer _devTrans;
        private readonly IMapper _mapper;

        public DeviceTransferController(IDeviceTransfer devTrans, IMapper mapper)
        {
            _devTrans = devTrans;
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
        Ok(await _devTrans.GetAllAsync());


        [HttpGet("GetbyId/{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
             Ok(await _devTrans.GetByIdAsync(id));



        [HttpPost("PostDeviceTransfer")]
        public async Task<IActionResult> Create(DeviceTransferDTO dto)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var DeviceTransfer = _mapper.Map<DeviceTransfer>(dto);
            var created = await _devTrans.CreateAsync(DeviceTransfer);
            var result = _mapper.Map<DeviceTransferDTO>(created);
            return Ok(new { data = result, message = "DeviceTransfer Added Successfully" });
        }

        [HttpPut("PutDeviceTransfer/{id}")]
        public async Task<IActionResult> Update(Guid id, DeviceTransferDTO DeviceTransfer)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");

            var updated = await _devTrans.UpdateAsync(id, DeviceTransfer);
            return Ok(new { data = updated, message = "DeviceTransfer Updated Successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //    return Unauthorized("User ID not found in token.");


            var existing = await _devTrans.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"❌ DeviceTransfer with ID {id} not found." });

            await _devTrans.DeleteAsync(id);

            return Ok(new { message = "✅ DeviceTransfer deleted successfully." });


        }

    }
}
