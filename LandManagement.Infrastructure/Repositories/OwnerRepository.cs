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
        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners
                .Where(owner => owner.IsActive == true)
                .ToListAsync();
        }
        public async Task<Owner?> GetOwnerDetailsByOwnerIdAsync(int ownerId)
        {
            return await _context.Owners
                .Where(owner => owner.Id == ownerId)
                .FirstOrDefaultAsync();
        }
        public async Task CreateNewOwnerAsync(Owner newOwner)
        {
            await _context.Owners.AddAsync(newOwner);
            await _context.SaveChangesAsync();
        }
    }
}
