using CarRentals.Repository.Interface;

namespace CarRentals.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookingRepository Bookings { get; }
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    ICarRepository Cars { get; }
    ICommentRepository Comments { get; }
    IRoleRepository Roles { get; }
    Task<int> SaveChangesAsync();
}