using LandManagement.Application.Interfaces.IRepositories;
using LandManagement.Domain.Entities;
using LandManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LandManagement.Infrastructure.Repositories
{
    public class LandRepository : ILandRepository
    {
        private readonly AppDbContext _context;
        public LandRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all active land records from database
        /// </summary>
        /// <returns>A list of active <see cref="Land"/>entities</returns>
        public async Task<IEnumerable<Land>> GetAllLandsAsync()
        {
            return await _context.Lands
                .Where(l => l.IsActive)
                .Include(l => l.Owner)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves land details by land identifier
        /// </summary>
        /// <param name="landId">The unique identifier of the land</param>
        /// <returns>The <see cref="Land"/>entity if found; otherwise, null</returns>
        public async Task<Land?> GetLandDetailsByLandIdAsync(int landId)
        {
            return await _context.Lands
                .Where(l => l.Id == landId)
                .Include(l => l.Owner)
                .FirstOrDefaultAsync();

        }

        /// <summary>
        /// Creates a new land record in the database
        /// </summary>
        /// <param name="newLand">The <see cref="Land/">entity containing new land details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task CreateNewLand(Land newLand)
        {
            await _context.Lands.AddAsync(newLand);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the details of an existing land record in the database
        /// </summary>
        /// <param name="updatedLand">The <see cref="Land/">entity containing updated land details</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateLandDetailsAsync(Land updatedLand)
        {
            _context.Lands.Update(updatedLand);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Inactivates a land record by setting its active status to false.
        /// </summary>
        /// <param name="landId">The unique identifier of the land to inactivate</param>
        /// <returns>The number of rows affected in the database</returns>
        public async Task<int> InactivateLandByLandIdAsync(int landId)
        {
            return await _context.Lands
                .Where(l => l.Id == landId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(l => l.IsActive, false)
                    .SetProperty(l => l.LastModifiedDateTime, DateTime.UtcNow)
                );
        }

        /// <summary>
        /// Checks if the owner is exists in the database
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>True if the owner exists; otherwise, false</returns>
        public async Task<bool> IsOwnerExists(int ownerId)
        {
            return await _context.Owners
                .AnyAsync(o => o.Id == ownerId);
        }
    }
}
