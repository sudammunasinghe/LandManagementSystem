using LandManagement.Application.DTOs.OwnerDTOs;

namespace LandManagement.Application.Interfaces.IServices
{
    public interface IOwnerService
    {
        /// <summary>
        /// Retrieves all active owner details
        /// </summary>
        /// <returns>A list of active owner records</returns>
        Task<IEnumerable<OwnerDto>> GetAllOwnersAsync();

        /// <summary>
        /// Retrieves owner details by owner identifier
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>The owner details if found; otherwise null</returns>
        Task<OwnerDto> GetOwnerDetailsByOwnerIdAsync(int ownerId);

        /// <summary>
        /// Creates a new owner record
        /// </summary>
        /// <param name="dto">The data transfer object containing owner details</param>
        /// <returns>The unique identifier of the newly created owner</returns>
        Task<int> CreateNewOwnerAsync(CreateOwnerDto dto);

        /// <summary>
        /// Updates the details of an existing owner record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated owner details</param>
        /// <returns>Returns true if the update was successful. otherwise, false</returns>
        Task<bool> UpdateOwnerDetailsAsync(UpdateOwnerDto dto);

        /// <summary>
        /// Inactivates a owner record by setting its active status to false.
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner to inactivate</param>
        /// <returns>
        /// Returns true if the owner was succesfully inactivated.
        /// Throws an exception if the owner does not exist.
        /// </returns>
        Task<bool> InactivateOwnerByOwnerIdAsync(int ownerId);
    }
}
