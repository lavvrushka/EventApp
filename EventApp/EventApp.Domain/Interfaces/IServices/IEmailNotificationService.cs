using EventApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventApp.Domain.Interfaces.IServices
{
    public interface IEmailNotificationService
    {
        /// <summary>
        /// Sends a notification email when an event is updated.
        /// </summary>
        /// <param name="updatedEvent">The event with updated details that will trigger the notification.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendEventUpdateNotificationAsync(Event updatedEvent);
    }
}
