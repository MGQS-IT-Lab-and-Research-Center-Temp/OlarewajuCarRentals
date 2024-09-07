using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface IBookingRepository : IRepository<Booking>
    {
       Task<Booking> GetBooking(Expression<Func<Booking, bool>> expression);
        Task<List<Booking>> GetAllBookings(Expression<Func<Booking, bool>> expression);
        Task<List<Booking>> GetAllBookings();
        Task<Booking> GetByReference(string reference);
    }
}
