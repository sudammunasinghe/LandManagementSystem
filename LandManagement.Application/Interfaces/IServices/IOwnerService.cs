using LandManagement.Application.DTOs.OwnerDTOs;

namespace LandManagement.Application.Interfaces.IServices
{
    public interface IOwnerService
    {
        Task<IEnumerable<OwnerDto>> GetAllOwnersAsync();
        Task<OwnerDto> GetOwnerDetailsByOwnerIdAsync(int ownerId);
        Task<bool> CreateNewOwnerAsync(CreateOwnerDto dto);
    }
}
