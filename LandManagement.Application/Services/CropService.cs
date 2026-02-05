using LandManagement.Application.DTOs.CropDTOs;
using LandManagement.Application.Interfaces.IRepositories;
using LandManagement.Application.Interfaces.IServices;
using LandManagement.Domain.Entities;

namespace LandManagement.Application.Services
{
    public class CropService : ICropService
    {
        private readonly ICropRepository _cropRepository;
        public CropService(ICropRepository cropRepository)
        {
            _cropRepository = cropRepository;
        }

        /// <summary>
        /// Retrieves all active crop details
        /// </summary>
        /// <returns>A list of active crop resords</returns>
        public async Task<IEnumerable<CropDto>> GetAllCropsAsync()
        {
            var allCrops = await _cropRepository.GetAllCropsAsync();
            var result = allCrops.Select(crop => new CropDto
            {
                CropId = crop.Id,
                Name = crop.Name
            }).ToList();
            return result;
        }

        /// <summary>
        /// Retrieves crop details by crop identifier
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop</param>
        /// <returns>The crop details if found;; otherwise null</returns>
        public async Task<CropDto?> GetCropDetailsByCropIdAsync(int cropId)
        {
            var crop = await _cropRepository.GetCropDetailsByCropIdAsync(cropId);
            var result = crop == null ? null : new CropDto
            {
                CropId = crop.Id,
                Name = crop.Name
            };
            return result;
        }

        /// <summary>
        /// Creates a new crop record
        /// </summary>
        /// <param name="dto">The data transfer object containing crop details</param>
        /// <returns>The unique identifier of the newly created crop</returns>
        public async Task<int> CreateNewCropAsync(CreateCropDto dto)
        {
            var newCrop = new Crop
            {
                Name = dto.Name
            };
            await _cropRepository.CreateNewCropAsync(newCrop);
            return newCrop.Id;
        }

        /// <summary>
        /// Updates the details of an existing crop record
        /// </summary>
        /// <param name="dto">The data transfer object containing updated crop information</param>
        /// <returns>Returns true if the update was successful. otherwise, false</returns>
        /// <exception cref="Exception">Thrown if the specified crop ID does not exist.</exception>
        public async Task<bool> UpdateCropDetailsAsync(UpdateCropDto dto)
        {
            var updatedCrop = await _cropRepository.GetCropDetailsByCropIdAsync(dto.Id);
            if (updatedCrop == null)
                throw new Exception($"Crop with Id {dto.Id} not found ...");
            updatedCrop.Name = dto.Name;

            await _cropRepository.UpdateCropDetailsAsync(updatedCrop);
            return true;
        }

        /// <summary>
        /// Inactivates a crop record by setting its active status to false.
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop to inactivate</param>
        /// <returns>
        /// Returns true if the crop was succesfully inactivated.
        /// Throws an exception if the crop does not exists.
        /// </returns>
        /// <exception cref="KeyNotFoundException">Thrown if the crop does not exist or no rows are affected.</exception>
        public async Task<bool> InactivateCropByCropIdAsync(int cropId)
        {
            var affectedRows = await _cropRepository.InactivateCropByCropIdAsync(cropId);
            if (affectedRows == 0)
                throw new KeyNotFoundException("Invalid crop Id ...");
            return true;
        }
    }
}
