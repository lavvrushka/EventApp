using EventApp.Domain.Models;

namespace EventApp.Application.Common.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user with the specified email, or null if not found.</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Registers a user for an event.
        /// </summary>
        /// <param name="idEvent">The identifier of the event.</param>
        /// <param name="idUser">The identifier of the user.</param>
        Task RegisterUserToEventAsync(Guid idEvent, Guid idUser);

        /// <summary>
        /// Retrieves the events for a user.
        /// </summary>
        /// <param name="id">The user's identifier.</param>
        /// <returns>The user with their events, or null if not found.</returns>
        Task<User?> GetUserEventsAsync(Guid id);

        /// <summary>
        /// Retrieves a list of users registered for an event.
        /// </summary>
        /// <param name="idEvent">The identifier of the event.</param>
        /// <returns>A list of users registered for the event.</returns>
        Task<List<User>> GetByEventAsync(Guid idEvent);

        /// <summary>
        /// Removes a user from an event.
        /// </summary>
        /// <param name="idUser">The user's identifier.</param>
        /// <param name="idEvent">The event's identifier.</param>
        Task DeleteInEventAsync(Guid idUser, Guid idEvent);

        /// <summary>
        /// Retrieves a paged list of users.
        /// </summary>
        /// <param name="pageSettings">The pagination settings.</param>
        /// <returns>A list of users for the current page.</returns>
        Task<List<User>> GetByPageAsync(PageSettings pageSettings);

        /// <summary>
        /// Retrieves the total count of users.
        /// </summary>
        /// <returns>The total number of users.</returns>
        Task<int> GetUserCountAsync();
    }
}
