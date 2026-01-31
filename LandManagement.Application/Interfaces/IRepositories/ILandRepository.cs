using LandManagement.Domain.Entities;

namespace LandManagement.Application.Interfaces.IRepositories
{
    public interface ILandRepository
    {
        Task<IEnumerable<Land>> GetAllLandsAsync();
        Task<Land> GetLandDetailsByLandIdAsync(int landId);
        Task CreateNewLand(Land newLand);
        Task UpdateLandDetailsAsync(Land updatedLand);
        Task<bool> IsOwnerExists(int ownerId);
        Task<bool> IsLandExists(int landId);
    }
}
