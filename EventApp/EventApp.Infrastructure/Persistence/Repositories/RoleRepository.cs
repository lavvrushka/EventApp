using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Models;
using EventApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
