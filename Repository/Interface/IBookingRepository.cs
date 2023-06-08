using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Booking GetBooking(Expression<Func<Booking, bool>> expression);
        List<Booking> GetAllBookings(Expression<Func<Booking, bool>> expression);
        Booking GetByReference(string reference);
    }
}
