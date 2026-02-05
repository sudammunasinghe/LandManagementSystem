using LandManagement.Application.DTOs.CropDTOs;

namespace LandManagement.Application.Interfaces.IServices
{
    public interface ICropService
    {
        /// <summary>
        /// Retrieves all active crop details
        /// </summary>
        /// <returns>A list of active crop resords</returns>
        Task<IEnumerable<CropDto>> GetAllCropsAsync();

        /// <summary>
        /// Retrieves crop details by crop identifier
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop</param>
        /// <returns>The crop details if found;; otherwise null</returns>
        Task<CropDto?> GetCropDetailsByCropIdAsync(int cropId);

        /// <summary>
        /// Creates a new crop record
        /// </summary>
        /// <param name="dto">The data transfer object containing crop details</param>
        /// <returns>The unique identifier of the newly created crop</returns>
        Task<int> CreateNewCropAsync(CreateCropDto dto);

        /// <summary>
        /// Updates the details of an existing land record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated crop information</param>
        /// <returns>Returns true if the update was successful. otherwise, false</returns>
        Task<bool> UpdateCropDetailsAsync(UpdateCropDto dto);

        /// <summary>
        /// Inactivates a crop record by setting its active status to false.
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop to inactivate</param>
        /// <returns>
        /// Returns true if the crop was succesfully inactivated.
        /// Throws an exception if the crop does not exist.
        /// </returns>
        Task<bool> InactivateCropByCropIdAsync(int cropId);
    }
}
