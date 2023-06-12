using CarRentals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentals.Context.EntityConfiguration
{
    public class PaymentCategoryConfig : IEntityTypeConfiguration<PaymentCategory>
    {
        public void Configure(EntityTypeBuilder<PaymentCategory> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.HasMany(bk => bk.Payments)
                .WithOne(c => c.PaymentCategory);
        }
    }
}
