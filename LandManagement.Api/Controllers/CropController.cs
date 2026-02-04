using LandManagement.Application.DTOs.CropDTOs;
using LandManagement.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LandManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropController : ControllerBase
    {
        private readonly ICropService _cropService;
        public CropController(ICropService cropService)
        {
            _cropService = cropService;
        }

        /// <summary>
        /// Retrieves all active crop details
        /// </summary>
        /// <returns>A list of active crop resords</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CropDto>>> GetAllCropsAsync()
        {
            var response = await _cropService.GetAllCropsAsync();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves crop details by crop identifier
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop</param>
        /// <returns>The crop details if found</returns>
        [HttpGet("{cropId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CropDto>> GetCropDetailsByCropIdAsync(int cropId)
        {
            var response = await _cropService.GetCropDetailsByCropIdAsync(cropId);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new crop record
        /// </summary>
        /// <param name="dto">The data transfer object containing crop details</param>
        /// <returns>The unique identifier of the newly created crop</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewCropAsync(CreateCropDto dto)
        {
            var result = await _cropService.CreateNewCropAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Updates the details of an existing crop record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated crop information</param>
        /// <returns>Returns true if the update was successful. otherwise, false or an appropriate error response</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCropDetailsAsync(UpdateCropDto dto)
        {
            var result = await _cropService.UpdateCropDetailsAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Inactivates a crop record by setting its active status to false.
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop to inactivate</param>
        /// <returns>
        /// Returns true if the crop was succesfully inactivated.
        /// Throws an exception if the crop does not exist.
        /// </returns>
        [HttpPut("{cropId}/Inactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> InactivateCropByCropIdAsync(int cropId)
        {
            var result = await _cropService.InactivateCropByCropIdAsync(cropId);
            return Ok(result);
        }
    }
}
