using LandManagement.Domain.Entities;

namespace LandManagement.Application.Interfaces.IRepositories
{
    public interface ILandRepository
    {
        /// <summary>
        /// Retrieves all active land records from database
        /// </summary>
        /// <returns>A list of active <see cref="Land"/>entities</returns>
        Task<IEnumerable<Land>> GetAllLandsAsync();

        /// <summary>
        /// Retrieves land details by land identifier
        /// </summary>
        /// <param name="landId">The unique identifier of the land</param>
        /// <returns>The <see cref="Land"/>entity if found; otherwise, null</returns>
        Task<Land> GetLandDetailsByLandIdAsync(int landId);

        /// <summary>
        /// Creates a new land record in the database
        /// </summary>
        /// <param name="newLand">The <see cref="Land/">entity containing new land details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task CreateNewLand(Land newLand);

        /// <summary>
        /// Updates the details of an existing land record in the database
        /// </summary>
        /// <param name="updatedLand">The <see cref="Land/">entity containing updated land details</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateLandDetailsAsync(Land updatedLand);

        /// <summary>
        /// Inactivates a land record by setting its active status to false.
        /// </summary>
        /// <param name="landId">The unique identifier of the land inactivate</param>
        /// <returns>The number of rows affected in the database</returns>
        Task<int> InactivateLandByLandIdAsync(int landId);

        /// <summary>
        /// Checks if the owner is exists in the database
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>True if the owner exists; otherwise, false</returns>
        Task<bool> IsOwnerExists(int ownerId);

        /// <summary>
        /// Checks if the land is exists in the database
        /// </summary>
        /// <param name="landId">The unique identifier of the land</param>
        /// <returns>True if the land exists; otherwise, false</returns>
        Task<bool> IsLandExists(int landId);
    }
}
