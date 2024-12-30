using EventApp.Application.DTOs.Email;

namespace EventApp.Application.Common.Interfaces.IServices
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously using the provided email details.
        /// </summary>
        /// <param name="emailDto">The data transfer object containing email details such as recipient, subject, and body.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendEmailAsync(EmailDto emailDto);
    }
}
