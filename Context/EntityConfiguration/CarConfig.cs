﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using   CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("text")
                .HasMaxLength(25);


            builder.Property(c => c.PlateNumber)
                .IsRequired()
                .HasColumnType("text")
                .HasMaxLength(11);


            builder.Property(c => c.Description)
              .HasColumnType("text")
              .HasMaxLength(11);

            builder.HasIndex(c => c.PlateNumber)
                  .IsUnique();

            builder.Property(c => c.ImageUrl)
                .HasColumnType("varchar(255)");

            builder.HasMany(bk => bk.Bookings)
                .WithOne(c => c.Car)
                .IsRequired();


            builder.HasMany(bk => bk.Comments)
                .WithOne(c => c.Car)
                .IsRequired();

            builder.HasMany(bk => bk.CarCategories)
                .WithOne(c => c.Car)
                .IsRequired();

        }
    }
}