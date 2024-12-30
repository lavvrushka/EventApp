using EventApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .IsRequired();

            builder.Property(e => e.DateTime)
                .IsRequired();

            builder.OwnsOne(e => e.Location, location =>
            {
                location.Property(l => l.Address).HasMaxLength(300);
                location.Property(l => l.City).IsRequired().HasMaxLength(100);
                location.Property(l => l.State).HasMaxLength(100);
                location.Property(l => l.Country).IsRequired().HasMaxLength(100);
            });

            builder.Property(e => e.Category)
                .IsRequired();

            builder.Property(e => e.MaxUsers)
                .IsRequired();

            builder.HasMany(e => e.Users)
                .WithMany(u => u.Events)
                .UsingEntity(j => j.ToTable("EventUsers"));
        }
    }

}
