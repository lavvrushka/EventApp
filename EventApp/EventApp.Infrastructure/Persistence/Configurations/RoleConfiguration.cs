﻿using EventApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasIndex(r => r.Name).IsUnique();

            builder.ToTable("Roles");
        }
    }
}