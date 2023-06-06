using CarRentals.Entities;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Context
{
    public class CarRentalsContext : DbContext
    {
        public CarRentalsContext(DbContextOptions<CarRentalsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).DateCreated = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                    ((BaseEntity)entry.Entity).LastModified = DateTime.Now;
            }

            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is ISoftDeletable && e.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChanges();
        }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<CarCategory> CarCategories { get; set; }
        DbSet<CarReport> CarrReports { get; set; }
        DbSet <Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet <User> Users { get; set; }
    
    }
}
