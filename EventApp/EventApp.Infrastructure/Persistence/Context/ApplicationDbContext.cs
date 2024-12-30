using EventApp.Domain.Models;
using EventApp.Infrastructure.Persistence.Comparers;
using EventApp.Infrastructure.Persistence.Configurations;
using EventApp.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Infrastructure.Persistence.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<RefreshToken> RefreshToken { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

            modelBuilder.Entity<Event>()
               .HasOne(e => e.Image)
               .WithMany()  
               .HasForeignKey(e => e.ImageId)
               .OnDelete(DeleteBehavior.SetNull);  
            var dictionaryComparer = new DictionaryValueComparer();
            modelBuilder.Entity<User>()
                .Property(u => u.EventRegistrationDates)
                .HasConversion(new DictionaryConverter())
                .Metadata.SetValueComparer(dictionaryComparer);  
        }

    }
}
