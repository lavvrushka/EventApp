using EventApp.Domain.Models;
using System.Linq.Expressions;

namespace EventApp.Domain.Intarfaces.IRepositories
{
    public interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Retrieves a list of events for users.
        /// </summary>
        Task<List<Event>> GetEventsUsersAsync();

        /// <summary>
        /// Retrieves an event by its title.
        /// </summary>
        Task<Event?> GetEventByTitleAsync(string title);

        /// <summary>
        /// Retrieves an event by its date.
        /// </summary>
        Task<Event?> GetEventByDateAsync(DateTime date);

        /// <summary>
        /// Retrieves a list of events sorted by category.
        /// </summary>
        Task<List<Event>> GetEventsSortedByCategoryAsync();

        /// <summary>
        /// Retrieves a list of events sorted by location.
        /// </summary>
        Task<List<Event>> GetEventsSortedByLocationAsync();

        /// <summary>
        /// Retrieves a list of events filtered by specified parameters.
        /// </summary>
        Task<List<Event>> GetFilteredAsync(Expression<Func<Event, bool>> filter);


        /// <summary>
        /// Retrieves a paged list of events based on the provided page settings.
        /// </summary>
        Task<List<Event>> GetByPageAsync(PageSettings pageSettings);

        /// <summary>
        /// Retrieves the total count of events.
        /// </summary>
        Task<int> GetEventCountAsync();
    }
}
