using EventApp.Application.Common.Interfaces.IRepositories;
using EventApp.Domain.Models;
using EventApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Infrastructure.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterUserToEventAsync(Guid idEvent, Guid idUser)
        {
            var user = await _context.Users
                .Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            var eventItem = await _context.Events
                .Include(e => e.Users)
                .FirstOrDefaultAsync(e => e.Id == idEvent);

            if (user != null && eventItem != null)
            {
                if (!eventItem.Users.Contains(user))
                {
                    eventItem.Users.Add(user);
                }

                if (!user.EventRegistrationDates.ContainsKey(idEvent))
                {
                    user.EventRegistrationDates[idEvent] = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserEventsAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Events)
                .ThenInclude(e => e.Image)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetByEventAsync(Guid idEvent)
        {
            var eventItem = await _context.Events.Include(e => e.Users).FirstOrDefaultAsync(e => e.Id == idEvent);
            return eventItem?.Users ?? new List<User>();
        }

        public async Task DeleteInEventAsync(Guid idUser, Guid idEvent)
        {
            var eventItem = await _context.Events.Include(e => e.Users).FirstOrDefaultAsync(e => e.Id == idEvent);
            var user = eventItem?.Users.FirstOrDefault(u => u.Id == idUser);

            if (user != null)
            {
                eventItem.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetByPageAsync(PageSettings pageSettings)
        {
            return await _context.Users
                .AsNoTracking()
                .Skip((pageSettings.PageIndex - 1) * pageSettings.PageSize)
                .Take(pageSettings.PageSize)
                .ToListAsync();
        }
        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();


        }

        public new async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == id);  

        }
    }
}
