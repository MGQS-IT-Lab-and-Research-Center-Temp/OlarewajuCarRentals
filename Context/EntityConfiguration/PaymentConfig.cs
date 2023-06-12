using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(bk => bk.PaymentCategory)
              .WithMany(c => c.Payments)
              .HasForeignKey(p=>p.PaymentCategoryId)
              .IsRequired();
        }
    }
}
