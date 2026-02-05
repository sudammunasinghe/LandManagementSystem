using LandManagement.Application.DTOs.LandDTOs;
using LandManagement.Application.DTOs.OwnerDTOs;
using LandManagement.Application.Interfaces.IRepositories;
using LandManagement.Application.Interfaces.IServices;
using LandManagement.Domain.Entities;

namespace LandManagement.Application.Services
{
    public class LandService : ILandService
    {
        private readonly ILandRepository _landRepository;
        public LandService(ILandRepository landRepository)
        {
            _landRepository = landRepository;
        }

        /// <summary>
        /// Retrieves all active land details
        /// </summary>
        /// <returns>A list of active land records</returns>
        public async Task<IEnumerable<LandDto>> GetAllLandsAsync()
        {
            var lands = await _landRepository.GetAllLandsAsync();
            var result = lands.Select(land => new LandDto
            {
                LandId = land.Id,
                Name = land.Name,
                Location = land.Location,
                DeedNumber = land.DeedNumber,
                SizeInAcres = land.SizeInAcres,
                OwnerDetails = land.Owner == null ? null : new OwnerDto
                {
                    OwnerId = land.Owner.Id,
                    FullName = $"{land.Owner.FirstName} {land.Owner.LastName}".Trim(),
                    NIC = land.Owner.NIC,
                    Address = land.Owner.Address,
                    Email = land.Owner.Email
                }
            }).ToList();
            return result;
        }

        /// <summary>
        /// Retrieves land details by land identifier
        /// </summary>
        /// <param name="landId">The unique identifier of the land</param>
        /// <returns>The land details if found; otherwiae  null</returns>
        public async Task<LandDto> GetLandDetailsByLandIdAsync(int landId)
        {
            var land = await _landRepository.GetLandDetailsByLandIdAsync(landId);
            if (land == null)
                return null;

            var result = land == null ? null : new LandDto
            {
                LandId = land.Id,
                Name = land.Name,
                Location = land.Location,
                DeedNumber = land.DeedNumber,
                SizeInAcres = land.SizeInAcres,
                OwnerDetails = land.Owner == null ? null : new OwnerDto
                {
                    OwnerId = land.Owner.Id,
                    FullName = $"{land.Owner.FirstName} {land.Owner.LastName}".Trim(),
                    NIC = land.Owner.NIC,
                    Address = land.Owner.Address,
                    Email = land.Owner.Email
                }
            };
            return result;
        }

        /// <summary>
        /// Creates a new land record
        /// </summary>
        /// <param name="dto">The data transfer object containing land details</param>
        /// <returns>The unique identifier of the newly created land</returns>
        /// <exception cref="Exception">Thrown if the specified owner does not exist.</exception>
        public async Task<int> CreateNewLand(CreateLandDto dto)
        {
            if (!await _landRepository.IsOwnerExists(dto.OwnerId))
                throw new Exception("Owner is not exists...");

            var newLand = new Land
            {
                Name = dto.Name,
                Location = dto.Location,
                DeedNumber = dto.DeedNumber,
                OwnerId = dto.OwnerId,
                SizeInAcres = dto.SizeInAcres
            };
            await _landRepository.CreateNewLand(newLand);
            return newLand.Id;
        }

        /// <summary>
        /// Updates the details of an existing land record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated land information</param>
        /// <returns>Returns true if the update was successful. otherwise, false</returns>
        /// <exception cref="Exception">Thrown if the specified land ID does not exist.</exception>
        public async Task<bool> UpdateLandDetailsAsync(UpdateLandDto dto)
        {
            var updatedLand = await _landRepository.GetLandDetailsByLandIdAsync(dto.Id);
            if (updatedLand == null)
                throw new Exception($"Land with Id {dto.Id} not found ...");

            updatedLand.Name = dto.Name;
            updatedLand.Location = dto.Location;
            updatedLand.DeedNumber = dto.DeedNumber;
            updatedLand.OwnerId = dto.OwnerId;
            updatedLand.SizeInAcres = dto.SizeInAcres;

            await _landRepository.UpdateLandDetailsAsync(updatedLand);
            return true;
        }

        /// <summary>
        /// Inactivates a land record by setting its active status to false.
        /// </summary>
        /// <param name="landId">The unique identifier of the land to inactivate</param>
        /// <returns>
        /// Returns true if the land was succesfully incativated.
        /// Throws an exception if the land does not exists.
        /// </returns>
        /// <exception cref="KeyNotFoundException">Thrown if the land does not exist or no rows are affected.</exception>
        public async Task<bool> InactivateLandByLandIdAsync(int landId)
        {
            var affectedRows = await _landRepository.InactivateLandByLandIdAsync(landId);
            if (affectedRows == 0)
                throw new KeyNotFoundException("Invalid Land Id ...");
            return true;
        }
    }
}
