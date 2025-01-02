using EventApp.Domain.Models;

namespace EventApp.Domain.Interfaces.IServices
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously using the provided email details.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendEmailAsync(EmailMassege emailDto);
    }
}
