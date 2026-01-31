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
                    FullName = land.Owner.FirstName + " " + land.Owner.LastName,
                    NIC = land.Owner.NIC,
                    Address = land.Owner.Address,
                    Email = land.Owner.Email
                }
            }).ToList();
            return result;
        }
        public async Task<LandDto> GetLandDetailsByLandIdAsync(int landId)
        {
            var land = await _landRepository.GetLandDetailsByLandIdAsync(landId);
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
        public async Task<bool> UpdateLandDetailsAsync(UpdateLandDto dto)
        {
            if (!await _landRepository.IsLandExists(dto.Id))
                throw new Exception("Invalid Land Id...");

            var updatedLand = new Land
            {
                Id = dto.Id,
                Name = dto.Name,
                Location = dto.Location,
                DeedNumber = dto.DeedNumber,
                OwnerId = dto.OwnerId,
                SizeInAcres = dto.SizeInAcres,
                LastModifiedDateTime = DateTime.UtcNow

            };
            await _landRepository.UpdateLandDetailsAsync(updatedLand);
            return true;
        }
    }
}
