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

        public async Task<IEnumerable<OwnerDto>> GetAllOwnersAsync()
        {
            var owners = await _ownerRepository.GetAllOwnersAsync();
            var result = owners.Select(o => new OwnerDto
            {
                OwnerId = o.Id,
                FullName = o.FirstName + " " + o.LastName,
                NIC = o.NIC,
                Address = o.Address,
                Email = o.Email
            });
            return result;
        }

        public async Task<OwnerDto> GetOwnerDetailsByOwnerIdAsync(int ownerId)
        {
            var owner = await _ownerRepository.GetOwnerDetailsByOwnerIdAsync(ownerId);
            var result = owner == null ? null : new OwnerDto
            {
                OwnerId = owner.Id,
                FullName = owner.FirstName + " " + owner.LastName,
                NIC = owner.NIC,
                Address = owner.Address,
                Email = owner.Email
            };
            return result;
        }

        public async Task<bool> CreateNewOwnerAsync(CreateOwnerDto dto)
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
            return true;
        }
    }
}
