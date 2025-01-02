using System.Net.Mail;
using System.Net;
using EventApp.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using EventApp.Domain.Interfaces.IServices;

namespace EventApp.Infrastructure.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;

        public EmailNotificationService(ILogger<EmailNotificationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _smtpClient = new SmtpClient(configuration["Email:SmtpHost"], int.Parse(configuration["Email:SmtpPort"]))
            {
                Credentials = new NetworkCredential(
                    configuration["Email:SmtpUser"],
                    configuration["Email:SmtpPassword"]),
                EnableSsl = true
            };
            _fromEmail = configuration["Email:FromEmail"];
        }

        public async Task SendEventUpdateNotificationAsync(Event updatedEvent)
        {
            if (updatedEvent.Users == null || !updatedEvent.Users.Any())
            {
                _logger.LogInformation($"No users to notify for event: {updatedEvent.Title}");
                return;
            }

            foreach (var user in updatedEvent.Users)
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = $"{updatedEvent.Title} - Event Updated",
                    Body = $"Hello {user.FirstName},\n\nThe event '{updatedEvent.Title}' you registered for has been updated.\n\nNew Details:\nDate: {updatedEvent.DateTime}\nLocation: {updatedEvent.Location.Address}, {updatedEvent.Location.City}, {updatedEvent.Location.State}, {updatedEvent.Location.Country}\nDescription: {updatedEvent.Description}\n\nBest regards,\nEvent Management Team",
                    IsBodyHtml = false
                };
                mailMessage.To.Add(user.Email);

                try
                {
                    await _smtpClient.SendMailAsync(mailMessage);
                    _logger.LogInformation($"Email sent to {user.Email} about event update: {updatedEvent.Title}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to send email to {user.Email}: {ex.Message}");
                }
            }
        }
    }
}      
