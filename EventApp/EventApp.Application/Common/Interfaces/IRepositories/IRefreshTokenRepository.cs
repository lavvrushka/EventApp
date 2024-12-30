using EventApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApp.Application.Common.Interfaces.IRepositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        /// <summary>
        /// Retrieves a refresh token by its token value.
        /// </summary>
        /// <param name="token">The refresh token string to search for.</param>
        Task<RefreshToken?> GetByTokenAsync(string token);

        /// <summary>
        /// Retrieves all refresh tokens associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(Guid userId);
    }
}
