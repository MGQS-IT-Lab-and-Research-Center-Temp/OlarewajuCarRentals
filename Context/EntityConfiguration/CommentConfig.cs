using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRentals.Entities;

namespace CarRentals.Context.EntityConfiguration
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder.ToTable("Comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentText)
                   .IsRequired()
                   .HasColumnType("text");

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.UserId)
                   .IsRequired();

            builder.HasOne(c => c.Car)
                   .WithMany(q => q.Comments)
                   .HasForeignKey(c => c.CarId)
                   .IsRequired();
        }
    }
}
