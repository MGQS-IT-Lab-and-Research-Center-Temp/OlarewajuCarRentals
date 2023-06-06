using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class CarCategoriesConfig : IEntityTypeConfiguration<CarCategory>
    {
        public void Configure(EntityTypeBuilder<CarCategory> builder)
        {

            builder.ToTable("CarCategories");

            builder.Ignore(cq => cq.Id);

            builder.HasKey(cq => new { cq.CarId, cq.CategoryId });

            builder.HasOne(cq => cq.Category)
                .WithMany(c => c.CarCategories)
                .HasForeignKey(cq => cq.CategoryId)
                .IsRequired();

            builder.HasOne(cq => cq.Car)
                .WithMany(q => q.CarCategories)
                .HasForeignKey(cq => cq.CarId)
                .IsRequired();
        }
    }
}
