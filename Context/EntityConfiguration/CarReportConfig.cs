using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class CarReportConfig : IEntityTypeConfiguration<CarReport>
    {
        public void Configure(EntityTypeBuilder<CarReport> builder)
        {
            builder.ToTable("CarReport");

            builder.HasKey(qr => qr.Id);

            builder.Property(qr => qr.AdditionalComment)
                    .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(qr => qr.Car)
                   .WithMany(q => q.CarReports)
                   .HasForeignKey(qr => qr.CarId)
                   .IsRequired();

            builder.HasOne(qr => qr.User)
                    .WithMany(u => u.CarReport)
                    .HasForeignKey(qr => qr.UserId)
                    .IsRequired();

        }
    }
}
