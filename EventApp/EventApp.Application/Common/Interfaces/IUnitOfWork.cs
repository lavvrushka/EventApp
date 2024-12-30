using EventApp.Application.Common.Interfaces.IRepositories;

namespace EventApp.Application.Common.Interfaces
{
    /// <summary>
    /// Represents a unit of work that provides repositories for interacting with entities and ensures atomic operations.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository for event entities.
        /// </summary>
        IEventRepository Events { get; }

        /// <summary>
        /// Gets the repository for role entities.
        /// </summary>
        IRoleRepository Roles { get; }

        /// <summary>
        /// Gets the repository for user entities.
        /// </summary>
        IUserRepository Users { get; }

        /// <summary>
        /// Gets the repository for refresh token entities.
        /// </summary>
        IRefreshTokenRepository RefreshTokens { get; }

        /// <summary>
        /// Gets the repository for image entities.
        /// </summary>
        IImageRepository Images { get; }

        /// <summary>
        /// Asynchronously saves changes made within the unit of work.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();
    }
}
