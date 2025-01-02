
namespace EventApp.Domain.Interfaces.IServices
{
    /// <summary>
    /// Interface for hashing passwords and verifying password hashes.
    /// </summary>
    public interface IHashPassword
    {
        /// <summary>
        /// Hashes a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password as a string.</returns>
        string Hash(string password);

        /// <summary>
        /// Verifies if the provided password matches the hashed password.
        /// </summary>
        /// <param name="hashedPassword">The previously hashed password.</param>
        /// <param name="providedPassword">The password to verify.</param>
        /// <returns>True if the passwords match; otherwise, false.</returns>
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
