using LandManagement.Application.Interfaces.IRepositories;
using LandManagement.Domain.Entities;
using LandManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LandManagement.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;
        public OwnerRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all active owner details from database
        /// </summary>
        /// <returns>A list of active <see cref="Owner"/>entities</returns>
        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners
                .Where(o => o.IsActive)
                .Include(o => o.Lands)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves owner details by owner identifier
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner</param>
        /// <returns>The <see cref="Owner/">entity if found; otherwise, null</returns>
        public async Task<Owner?> GetOwnerDetailsByOwnerIdAsync(int ownerId)
        {
            return await _context.Owners
                .Where(o => o.Id == ownerId)
                .Include(o => o.Lands)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///  Creates a new land record in the database
        /// </summary>
        /// <param name="newOwner">The <see cref="Owner"/>entity containing new owner details</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task CreateNewOwnerAsync(Owner newOwner)
        {
            await _context.Owners.AddAsync(newOwner);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the details of an existing owner record in the database
        /// </summary>
        /// <param name="updatedOwner">The <see cref="Owner"/>entity containing updated owner details</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateOwnerDetailsAsync(Owner updatedOwner)
        {
            _context.Owners.Update(updatedOwner);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Inactivates a owner record by setting its active status to false.
        /// </summary>
        /// <param name="ownerId">The unique identifier of the owner to inactivate</param>
        /// <returns>The number of rows affected in the database</returns>
        public async Task<int> InactivateOwnerByOwnerIdAsync(int ownerId)
        {
            return await _context.Owners
                .Where(o => o.Id == ownerId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.IsActive, false)
                    .SetProperty(s => s.LastModifiedDateTime, DateTime.UtcNow)
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
