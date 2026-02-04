using LandManagement.Application.Interfaces.IRepositories;
using LandManagement.Domain.Entities;
using LandManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LandManagement.Infrastructure.Repositories
{
    public class CropRepository : ICropRepository
    {
        private readonly AppDbContext _context;
        public CropRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all active crop records from database
        /// </summary>
        /// <returns>A list of active <see cref="Crop"/>entities</returns>
        public async Task<IEnumerable<Crop>> GetAllCropsAsync()
        {
            return await _context.Crops
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves crop details by crop identifier
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop</param>
        /// <returns>The <see cref="Crop"/>entity if found; otherwise null</returns>
        public async Task<Crop?> GetCropDetailsByCropIdAsync(int cropId)
        {
            return await _context.Crops
                .Where(c => c.Id == cropId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates a new crop record in the database
        /// </summary>
        /// <param name="newCrop">The <see cref="Crop"/>entity containing updated crop details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task CreateNewCropAsync(Crop newCrop)
        {
            await _context.Crops.AddAsync(newCrop);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the details of an existing crop record in the database
        /// </summary>
        /// <param name="updatedCrop">The <see cref="Crop/">entity containing updated crop details</param>
        /// <returns>>A task representing the asynchronous operation.</returns>
        public async Task UpdateCropDetailsAsync(Crop updatedCrop)
        {
            _context.Crops.Update(updatedCrop);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Inactivates a crop record by setting its active status to false.
        /// </summary>
        /// <param name="cropId">The unique identifier of the crop to inactivate</param>
        /// <returns>The number of rows affected in the database</returns>
        public async Task<int> InactivateCropByCropIdAsync(int cropId)
        {
            return await _context.Crops
                .Where(c => c.Id == cropId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.IsActive, false)
                    .SetProperty(c => c.LastModifiedDateTime, DateTime.UtcNow)
                );
        }
    }
}
