using LandManagement.Domain.Entities;

namespace LandManagement.Application.Interfaces.IRepositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Task<Owner> GetOwnerDetailsByOwnerIdAsync(int ownerId);
        Task CreateNewOwnerAsync(Owner newOwner);
    }
}
