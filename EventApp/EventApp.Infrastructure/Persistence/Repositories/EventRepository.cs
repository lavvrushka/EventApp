using EventApp.Domain.Models;
using EventApp.Application.Common.Interfaces.IRepositories;
using EventApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Event>> GetEventsFilteredAsync(string? address = null, string? city = null, string? state = null, string? country = null, string? category = null)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(e => e.Location.City == city);
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(e => e.Location.Country == country);
            }
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(e => e.Location.Address == address);
            }
            if (!string.IsNullOrEmpty(state))
            {
                query = query.Where(e => e.Location.State == state);
            }
            query = query.Where(x => string.IsNullOrEmpty(category) || x.Category.Trim().ToLower().Contains(category.Trim().ToLower()));


            return await query.ToListAsync();
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
