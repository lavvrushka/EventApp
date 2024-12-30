using EventApp.Application.Common.Interfaces.IRepositories;
using EventApp.Application.Common.Interfaces;
using EventApp.Infrastructure.Persistence.Context;
using EventApp.Infrastructure.Persistence.Repositories;

namespace EventApp.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IEventRepository? _eventRepository;
        private IUserRepository? _userRepository;
        private IRoleRepository? _roleRepository;
        private IRefreshTokenRepository? _refreshTokenRepository;
        private IImageRepository? _imageRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEventRepository Events => _eventRepository ??= new EventRepository(_context);
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_context);
        public IImageRepository Images => _imageRepository ??= new ImageRepository(_context);
        public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
