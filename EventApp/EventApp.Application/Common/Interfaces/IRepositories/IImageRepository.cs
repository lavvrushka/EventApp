using EventApp.Domain.Models;

namespace EventApp.Application.Common.Interfaces.IRepositories
{
    public interface IImageRepository : IRepository<Image>
    {
        /// <summary>
        /// Adds an image to an event and returns the image's unique identifier.
        /// </summary>
        /// <param name="entity">The image entity to be added.</param>
        Task<Guid> AddImageToEventAsync(Image entity);
    }
}
