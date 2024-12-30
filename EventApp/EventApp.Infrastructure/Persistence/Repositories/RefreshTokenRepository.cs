using EventApp.Application.Common.Interfaces.IRepositories;
using EventApp.Domain.Models;
using EventApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context) { }
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.Set<RefreshToken>()
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }
        public async Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Set<RefreshToken>()
                .Where(rt => rt.UserId == userId)
                .ToListAsync();
        }
    }
}
