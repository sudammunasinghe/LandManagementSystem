using LandManagement.Application.DTOs.LandDTOs;

namespace LandManagement.Application.Interfaces.IServices
{
    public interface ILandService
    {
        /// <summary>
        ///  Retrieves all active land details
        /// </summary>
        /// <returns>A list of active land records</returns>
        Task<IEnumerable<LandDto>> GetAllLandsAsync();

        /// <summary>
        /// Retrieves land details by land identifier
        /// </summary>
        /// <param name="landId">The unique identifier of the land</param>
        /// <returns>The land details if found; otherwiae  null</returns> 
        Task<LandDto> GetLandDetailsByLandIdAsync(int landId);

        /// <summary>
        /// Creates a new land record
        /// </summary>
        /// <param name="dto">The data transfer object containing land details</param>
        /// <returns>The unique identifier of the newly created land</returns>
        Task<int> CreateNewLand(CreateLandDto dto);

        /// <summary>
        /// Updates the details of an existing land record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated land information</param>
        /// <returns>Returns true if the update was successful. otherwise, false</returns>
        Task<bool> UpdateLandDetailsAsync(UpdateLandDto dto);

        /// <summary>
        /// Inactivates a land record by setting its active status to false.
        /// </summary>
        /// <param name="landId">The unique identifier of the land to inactivate</param>
        /// <returns>
        /// Returns true if the land was succesfully incativated.
        /// Throws an exception if the land does not exists.
        /// </returns>
        Task<bool> InactivateLandByLandIdAsync(int landId);
    }
}
