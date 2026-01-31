using LandManagement.Application.DTOs.LandDTOs;

namespace LandManagement.Application.Interfaces.IServices
{
    public interface ILandService
    {
        Task<IEnumerable<LandDto>> GetAllLandsAsync();
        Task<LandDto> GetLandDetailsByLandIdAsync(int landId);
        Task<int> CreateNewLand(CreateLandDto dto);
        Task<bool> UpdateLandDetailsAsync(UpdateLandDto dto);
    }
}
