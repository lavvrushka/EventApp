using EventApp.Domain.Models;

namespace EventApp.Domain.Intarfaces.IRepositories
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
