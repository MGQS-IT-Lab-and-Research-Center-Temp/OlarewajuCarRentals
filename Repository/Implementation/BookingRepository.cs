using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRentals.Repository.Implementation
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(CarRentalsContext context) : base(context)
        {
        }
        public async Task<Booking> GetBooking(Expression<Func<Booking, bool>> expression)
        {
            var booking = await  _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Car)
                .SingleOrDefault(expression);
            return booking;
        }

        public List<Booking> GetAllBookings(Expression<Func<Booking, bool>> expression)
        {
            var bookings = _context.Bookings.Where(expression).Include(b => b.User).Include(b => b.Car).ToList();
            return bookings;
        }
        public List<Booking> GetAllBookings()
        {
            var bookings = _context.Bookings.Include(b => b.User).Include(b => b.Car).ToList();
            return bookings;
        }


        public Booking GetByReference(string reference)
        {
            var booking = _context.Bookings.Include(b => b.User).Include(b => b.Car).SingleOrDefault(b => b.BookingReference == reference);
            return booking;
        }

    }
}
