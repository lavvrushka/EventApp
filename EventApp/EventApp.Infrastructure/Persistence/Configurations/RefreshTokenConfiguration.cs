using EventApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(500); 
            builder.Property(rt => rt.Expires)
                .IsRequired();
            builder.Property(rt => rt.Created)
                .IsRequired();
            builder.HasOne(rt => rt.User)
                .WithMany()  
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
            builder.HasIndex(rt => rt.UserId);  
            builder.HasIndex(rt => rt.Expires);  
        }
    }
}
