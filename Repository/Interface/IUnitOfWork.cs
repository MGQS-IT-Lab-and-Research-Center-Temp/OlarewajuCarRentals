using CarRentals.Repository.Interface;

namespace CarRentals.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookingRepository Bookings { get; }
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    ICarRepository Cars { get; }
    ICommentRepository Comments { get; }
    ICarReportRepository CarReports { get; }
    IRoleRepository Roles { get; }
    int SaveChanges();
}