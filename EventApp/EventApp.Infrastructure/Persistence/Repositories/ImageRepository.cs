using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Models;
using EventApp.Infrastructure.Persistence.Context;

namespace EventApp.Infrastructure.Persistence.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Guid> AddImageToEventAsync(Image image)
        {
            await _context.Set<Image>().AddAsync(image);
            return image.Id;
        }
    }
}
