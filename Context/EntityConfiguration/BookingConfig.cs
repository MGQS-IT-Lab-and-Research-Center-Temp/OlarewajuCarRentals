using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(bk => bk.Id);

            builder.HasOne(bk => bk.Car)
                  .WithMany(c => c.Bookings)
                  .HasForeignKey(bk => bk.CarId);

            builder.HasOne(bk => bk.User)
                 .WithMany(c => c.Bookings)
                 .HasForeignKey(bk => bk.UserId);


            builder.HasOne(bk => bk.Payment)
                  .WithMany(c => c.Bookings)
                  .HasForeignKey(bk => bk.PaymentId);

            builder.Property(bk => bk.Amount)
                   .IsRequired()
                   .HasColumnType("Decimal");

     
   





        }
    }
}
