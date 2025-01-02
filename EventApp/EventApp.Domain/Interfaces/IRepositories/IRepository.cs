namespace EventApp.Domain.Intarfaces.IRepositories
{
    /// <summary>
    /// Generic repository interface for basic entity operations.
    /// Includes methods for retrieving all entities, getting by ID, adding, updating, and deleting.
    /// </summary>
    /// <typeparam name="T">The type of the entity that the repository works with.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A collection of all entities of type T.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity with the updated data.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        Task DeleteAsync(T entity);
    }
}
