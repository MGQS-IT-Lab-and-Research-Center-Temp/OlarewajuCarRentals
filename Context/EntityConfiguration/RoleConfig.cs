﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoleName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(r => r.RoleName)
                     .IsUnique();

            builder.Property(r => r.Description)
                   .HasMaxLength(200);

            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role)
                   .HasForeignKey(u => u.RoleId);
        }
    }
    
}
