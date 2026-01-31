using LandManagement.Application.DTOs.LandDTOs;
using LandManagement.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LandManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LandController : ControllerBase
    {
        private readonly ILandService _landService;
        public LandController(ILandService landService)
        {
            _landService = landService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LandDto>>> GetAllLandsAsync()
        {
            var response = await _landService.GetAllLandsAsync();
            return Ok(response);
        }

        [HttpGet("landId")]
        public async Task<ActionResult<LandDto>> GetLandDetailsByLandIdAsync(int landId)
        {
            var response = await _landService.GetLandDetailsByLandIdAsync(landId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewLand(CreateLandDto dto)
        {
            var landId = await _landService.CreateNewLand(dto);
            return Ok(landId);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLandDetailsAsync(UpdateLandDto dto)
        {
            var result = await _landService.UpdateLandDetailsAsync(dto);
            return Ok(result);
        }
    }
}
