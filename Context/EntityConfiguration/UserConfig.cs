using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(11);


            builder.HasIndex(u => u.Phone)
                   .IsUnique();


            builder.Property(u => u.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(u => u.Email)
                .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();
                
            builder.Property(u => u.RoleId)
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }


}
