using LandManagement.Application.DTOs.LandDTOs;
using LandManagement.Application.DTOs.OwnerDTOs;
using LandManagement.Application.Interfaces.IRepositories;
using LandManagement.Application.Interfaces.IServices;
using LandManagement.Domain.Entities;

namespace LandManagement.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        /// <summary>
        /// Retrieves all active owner details
        /// </summary>
        /// <returns>A list of active owner records</returns>
        public async Task<IEnumerable<OwnerDto>> GetAllOwnersAsync()
        {
            var owners = await _ownerRepository.GetAllOwnersAsync();
            var result = owners.Select(o => new OwnerDto
            {
                OwnerId = o.Id,
                FullName = $"{o.FirstName} {o.LastName}".Trim(),
                NIC = o.NIC,
                Address = o.Address,
                Email = o.Email,
                LandDetails = o.Lands.Select(l => new LandDto
                {
                    LandId = l.Id,
                    Name = l.Name,
                    Location = l.Location,
                    DeedNumber = l.DeedNumber,
                    SizeInAcres = l.SizeInAcres
                }).ToList()
            });
            return result;
        }

        /// <summary>
        /// Retrieves owner details by owner identifier
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>The owner details if found; otherwise null</returns>
        public async Task<OwnerDto> GetOwnerDetailsByOwnerIdAsync(int ownerId)
        {
            var owner = await _ownerRepository.GetOwnerDetailsByOwnerIdAsync(ownerId);
            var result = owner == null ? null : new OwnerDto
            {
                OwnerId = owner.Id,
                FullName = $"{owner.FirstName} {owner.LastName}".Trim(),
                NIC = owner.NIC,
                Address = owner.Address,
                Email = owner.Email,
                LandDetails = owner.Lands.Select(l => new LandDto
                {
                    LandId = l.Id,
                    Name = l.Name,
                    Location = l.Location,
                    DeedNumber = l.DeedNumber,
                    SizeInAcres = l.SizeInAcres
                }).ToList()
            };
            return result;
        }

        /// <summary>
        /// Creates a new owner record
        /// </summary>
        /// <param name="dto">The data transfer object containing owner details</param>
        /// <returns>The unique identifier of the newly created owner</returns>
        public async Task<int> CreateNewOwnerAsync(CreateOwnerDto dto)
        {
            var newOwner = new Owner
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NIC = dto.NIC,
                Address = dto.Address,
                Email = dto.Email
            };
            await _ownerRepository.CreateNewOwnerAsync(newOwner);
            return newOwner.Id;
        }

        /// <summary>
        /// Updates the details of an existing owner record
        /// </summary>
        /// <param name="dto">The data transfer object conataining updated owner information</param>
        /// <returns>Returns true if the update was successful. otherwise, false</returns>
        /// <exception cref="Exception">Thrown if the specified owner ID does not exist</exception>
        public async Task<bool> UpdateOwnerDetailsAsync(UpdateOwnerDto dto)
        {
            var updatedOwner = await _ownerRepository.GetOwnerDetailsByOwnerIdAsync(dto.Id);
            if (updatedOwner == null)
                throw new Exception($"Owner with Id {dto.Id} not found ...");

            updatedOwner.FirstName = dto.FirstName;
            updatedOwner.LastName = dto.LastName;
            updatedOwner.NIC = dto.NIC;
            updatedOwner.Address = dto.Address;
            updatedOwner.Email = dto.Email;

            await _ownerRepository.UpdateOwnerDetailsAsync(updatedOwner);
            return true;
        }

        /// <summary>
        /// Inactivates a owner record by setting its active status to false.
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner to inactivate</param>
        /// <returns>
        /// Returns true if the owner was succesfully inactivated.
        /// Throws an exception if the owner does not exist.
        /// </returns>
        /// <exception cref="KeyNotFoundException">Thrown if the owner does not exist or no rows are affected.</exception>
        public async Task<bool> InactivateOwnerByOwnerIdAsync(int ownerId)
        {
            var affectedRows = await _ownerRepository.InactivateOwnerByOwnerIdAsync(ownerId);
            if (affectedRows == 0)
                throw new KeyNotFoundException("Invalid Owner Id ...");
            return true;
        }
    }
}
