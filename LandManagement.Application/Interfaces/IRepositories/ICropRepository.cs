using LandManagement.Domain.Entities;

namespace LandManagement.Application.Interfaces.IRepositories
{
    public interface ICropRepository
    {
        /// <summary>
        /// Retrieves all active crop records from database
        /// </summary>
        /// <returns>A list of active <see cref="Crop"/>entities</returns>
        Task<IEnumerable<Crop>> GetAllCropsAsync();

        /// <summary>
        /// Retrieves crop details by crop identifier
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop</param>
        /// <returns>The <see cref="Crop"/>entity if found; otherwise null</returns>
        Task<Crop?> GetCropDetailsByCropIdAsync(int cropId);

        /// <summary>
        /// Creates a new crop record in the database
        /// </summary>
        /// <param name="newCrop">The <see cref="Crop"/>entity containing updated crop details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task CreateNewCropAsync(Crop newCrop);

        /// <summary>
        /// Updates the details of an existing crop record in the database
        /// </summary>
        /// <param name="updatedCrop">The <see cref="Crop/">entity containing updated crop details</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateCropDetailsAsync(Crop updatedCrop);

        /// <summary>
        /// Inactivates a crop record by setting its active status to false.
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop to inactivate</param>
        /// <returns>The number of rows affected in the database</returns>
        Task<int> InactivateCropByCropIdAsync(int cropId);
    }
}
