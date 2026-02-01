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

        /// <summary>
        /// Retrieves all active land details
        /// </summary>
        /// <returns>A list of active land records</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LandDto>>> GetAllLandsAsync()
        {
            var response = await _landService.GetAllLandsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves land details by land identifier
        /// </summary>
        /// <param name="landId">The unique identifier of the land</param>
        /// <returns>The land details if found</returns>
        [HttpGet("{landId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LandDto>> GetLandDetailsByLandIdAsync(int landId)
        {
            var response = await _landService.GetLandDetailsByLandIdAsync(landId);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new land record
        /// </summary>
        /// <param name="dto">The data transfer object containing land details</param>
        /// <returns>The unique identifier of the newly created land</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewLand(CreateLandDto dto)
        {
            var landId = await _landService.CreateNewLand(dto);
            return Ok(landId);
        }

        /// <summary>
        /// Updates the details of an existing land record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated land information</param>
        /// <returns>Returns true if the update was successful. otherwise, false or an appropriate error response</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateLandDetailsAsync(UpdateLandDto dto)
        {
            var result = await _landService.UpdateLandDetailsAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Inactivates a land record by setting its active status to false.
        /// </summary>
        /// <param name="landId">The unique identifier of the land to inactivate</param>
        /// <returns>
        /// Returns true if the land was succesfully inactivated.
        /// Throws an exception if the land does not exist.
        /// </returns>
        [HttpPut("{landId}/inactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> InactivateLandByLandIdAsync(int landId)
        {
            var result = await _landService.InactivateLandByLandIdAsync(landId);
            return Ok(result);
        }
    }
}
