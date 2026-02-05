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

        /// <summary>
        /// Retrieves all active owner details
        /// </summary>
        /// <returns>A list of active owner records</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAllOwnersAsync()
        {
            var response = await _ownerService.GetAllOwnersAsync();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves owner details by owner identifier
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>The owner details if found</returns>
        [HttpGet("{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OwnerDto>> GetOwnerDetailsByOwnerIdAsync(int ownerId)
        {
            var response = await _ownerService.GetOwnerDetailsByOwnerIdAsync(ownerId);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new owner record
        /// </summary>
        /// <param name="dto">The data transfer object containing owner details</param>
        /// <returns>The unique identifier of the newly created owner</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewOwnerAsync(CreateOwnerDto dto)
        {
            var result = await _ownerService.CreateNewOwnerAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Updates the details of an existing owner record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated owner details</param>
        /// <returns>Returns true if the update was successful. otherwise, false or an appropriate error response</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateOwnerDetailsAsync(UpdateOwnerDto dto)
        {
            var result = await _ownerService.UpdateOwnerDetailsAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Inactivates a owner record by setting its active status to false.
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner to inactivate</param>
        /// <returns>
        /// Returns true if the owner was succesfully inactivated.
        /// Throws an exception if the owner does not exist.
        /// </returns>
        [HttpPut("{ownerId}/inactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> InactivateOwnerByOwnerIdAsync(int ownerId)
        {
            var result = await _ownerService.InactivateOwnerByOwnerIdAsync(ownerId);
            return Ok(result);
        }
    }
}
