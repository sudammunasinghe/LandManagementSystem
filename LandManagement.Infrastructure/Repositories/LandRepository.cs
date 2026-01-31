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

        public async Task<IEnumerable<Land>> GetAllLandsAsync()
        {
            return await _context.Lands
                .Where(l => l.IsActive == true)
                .Include(l => l.Owner)
                .ToListAsync();
        }
        public async Task<Land?> GetLandDetailsByLandIdAsync(int landId)
        {
            return await _context.Lands
                .Where(land => land.Id == landId)
                .Include(land => land.Owner)
                .FirstOrDefaultAsync();

        }
        public async Task CreateNewLand(Land newLand)
        {
            await _context.Lands.AddAsync(newLand);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLandDetailsAsync(Land updatedLand)
        {
            _context.Lands.Update(updatedLand);
            await _context.SaveChangesAsync();

        }
        public async Task<bool> IsOwnerExists(int ownerId)
        {
            return await _context.Owners.AnyAsync(o => o.Id == ownerId);
        }
        public async Task<bool> IsLandExists(int landId)
        {
            return await _context.Lands
                 .AnyAsync(land => land.Id == landId);
        }
    }
}
