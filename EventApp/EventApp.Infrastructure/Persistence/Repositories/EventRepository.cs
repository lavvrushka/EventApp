using EventApp.Domain.Models;
using EventApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using EventApp.Domain.Intarfaces.IRepositories;
using System.Linq.Expressions;

namespace EventApp.Infrastructure.Persistence.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context) {}

        public new async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events
            .Include(e => e.Image)   
            .Include(e => e.Users)   
            .ToListAsync();
               
        }

        public new async Task<Event> GetByIdAsync(Guid id)
        {
            return await _context.Events
                .Include(e => e.Image) 
                .Include(e => e.Users)  
                .FirstOrDefaultAsync(e => e.Id == id); 
        }

        public async Task<List<Event>> GetEventsUsersAsync()
        {
            return await _context.Events.Include(e => e.Users).ToListAsync();
        }

        public async Task<Event?> GetEventByTitleAsync(string title)
        {
            return await _context.Events.Include(e => e.Image).Include(e => e.Users).Include(e => e.Users).FirstOrDefaultAsync(e => e.Title == title);
        }

        public async Task<Event?> GetEventByDateAsync(DateTime date)
        {
            return await _context.Events.Include(e => e.Image).Include(e => e.Users).FirstOrDefaultAsync(e => e.DateTime == date);
        }

        public async Task<List<Event>> GetEventsSortedByCategoryAsync()
        {
            return await _context.Events.Include(e => e.Image).Include(e => e.Users).OrderBy(e => e.Category).ToListAsync();
        }

        public async Task<List<Event>> GetEventsSortedByLocationAsync()
        {
            return await _context.Events.Include(e => e.Image).Include(e => e.Users).OrderBy(e => e.Location.City).ToListAsync();
        }

        public async Task<List<Event>> GetFilteredAsync(Expression<Func<Event, bool>> filter)
        {
            return await _context.Events
                .Where(filter)
                .Include(e => e.Image)
                .Include(e => e.Users)
                .ToListAsync();
        }


        public async Task<List<Event>> GetByPageAsync(PageSettings pageSettings)
        {
            return await _context.Events
                .Include(e => e.Image)
                .Include(e => e.Users)
                .AsNoTracking()
                .Skip((pageSettings.PageIndex - 1) * pageSettings.PageSize)
                .Take(pageSettings.PageSize)
                .ToListAsync();
        }
        public async Task<int> GetEventCountAsync()
        {
            return await _context.Events.CountAsync();
        }
    }
}
