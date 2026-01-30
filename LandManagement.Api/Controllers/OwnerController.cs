using LandManagement.Application.DTOs.OwnerDTOs;
using LandManagement.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LandManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAllOwnersAsync()
        {
            var response = await _ownerService.GetAllOwnersAsync();
            return Ok(response);
        }

        [HttpGet("ownerId")]
        public async Task<ActionResult<OwnerDto>> GetOwnerDetailsByOwnerIdAsync(int ownerId)
        {
            var response = await _ownerService.GetOwnerDetailsByOwnerIdAsync(ownerId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewOwnerAsync(CreateOwnerDto dto)
        {
            var result = await _ownerService.CreateNewOwnerAsync(dto);
            return Ok(result);
        }
    }
}
