using CarRentals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentals.Context.EntityConfiguration
{
    public class CarGalleryConfig : IEntityTypeConfiguration<CarGallery>

    {
        public void Configure(EntityTypeBuilder<CarGallery> builder)
        {
            builder.HasKey(cg => cg.Id);

            builder.HasOne(c => c.Car)
                   .WithMany(q => q.CarGalleries)
                   .HasForeignKey(c => c.CarId)
                   .IsRequired();
        }
    }
}
