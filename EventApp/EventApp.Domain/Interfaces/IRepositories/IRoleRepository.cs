using EventApp.Domain.Models;

namespace EventApp.Domain.Intarfaces.IRepositories
{
    /// <summary>
    /// Repository interface for working with roles.
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Retrieves a role by its name.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>The role object, or null if the role is not found.</returns>
        Task<Role?> GetByNameAsync(string name);
    }
}
