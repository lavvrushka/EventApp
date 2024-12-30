using EventApp.Domain.Models;

namespace EventApp.Application.Common.Interfaces.IServices
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates an access token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the access token will be generated.</param>
        /// <returns>A task that represents the asynchronous operation, containing the generated access token.</returns>
        Task<string> GenerateAccessToken(User user);

        /// <summary>
        /// Generates a refresh token for the specified user by their user ID.
        /// </summary>
        /// <param name="userId">The user ID for whom the refresh token will be generated.</param>
        /// <returns>A task that represents the asynchronous operation, containing the generated refresh token.</returns>
        Task<string> GenerateRefreshTokenAsync(Guid userId);

        /// <summary>
        /// Revokes the refresh token of the currently logged-in user.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RevokeRefreshTokenAsync();

        /// <summary>
        /// Refreshes both the access token and the refresh token using the provided token.
        /// </summary>
        /// <param name="token">The current refresh token to be used for generating new tokens.</param>
        /// <returns>A task representing the asynchronous operation, containing the new access token and refresh token.</returns>
        Task<(string newAccessToken, string newRefreshToken)> RefreshTokensAsync(string token);

        /// <summary>
        /// Extracts the user ID from the given token.
        /// </summary>
        /// <param name="token">The token from which the user ID will be extracted.</param>
        /// <returns>The user ID extracted from the token, or null if the extraction fails.</returns>
        Guid? ExtractUserIdFromToken(string token);

        /// <summary>
        /// Extracts the token from the request header.
        /// </summary>
        /// <returns>The token extracted from the header, or null if no token is found.</returns>
        string? ExtractTokenFromHeader();
    }
}
