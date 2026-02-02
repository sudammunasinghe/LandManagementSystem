using LandManagement.Domain.Entities;

namespace LandManagement.Application.Interfaces.IRepositories
{
    public interface IOwnerRepository
    {
        /// <summary>
        /// Retrieves all active owner details from database
        /// </summary>
        /// <returns>A list of active <see cref="Owner"/>entities</returns>
        Task<IEnumerable<Owner>> GetAllOwnersAsync();

        /// <summary>
        /// Retrieves owner details by owner identifier
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>The <see cref="Owner/">entity if found; otherwise, null</returns>
        Task<Owner> GetOwnerDetailsByOwnerIdAsync(int ownerId);

        /// <summary>
        /// Creates a new land record in the database
        /// </summary>
        /// <param name="newOwner">The <see cref="Owner"/>entity containing new owner details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task CreateNewOwnerAsync(Owner newOwner);

        /// <summary>
        /// Updates the details of an existing owner record in the database
        /// </summary>
        /// <param name="updatedOwner">The <see cref="Owner"/>entity containing updated owner details</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateOwnerDetailsAsync(Owner updatedOwner);

        /// <summary>
        /// Inactivates a owner record by setting its active status to false.
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner to inactivate</param>
        /// <returns>The number of rows affected in the database</returns>
        Task<int> InactivateOwnerByOwnerIdAsync(int ownerId);

        /// <summary>
        /// Checks if the owner is exists in the database
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>True if the owner exists; otherwise, false</returns>
        Task<bool> IsOwnerExists(int ownerId);
    }
}
