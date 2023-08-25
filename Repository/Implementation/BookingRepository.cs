using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRentals.Repository.Implementation;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(CarRentalsContext context) : base(context)
    {
    }
    public async Task<Booking> GetBooking(Expression<Func<Booking, bool>> expression)
    {
        var booking = await _context.Bookings
            .Include(b => b.User)
             .Include(b => b.Car)
            .SingleOrDefaultAsync(expression);
        return booking;
    }

    public async Task<List<Booking>> GetAllBookings(Expression<Func<Booking, bool>> expression)
    {
        var bookings = await _context.Bookings.Where(expression)
            .Include(b => b.User)
            .Include(b => b.Car)
            .ToListAsync();
        return bookings;
    }
    public async Task<List<Booking>> GetAllBookings()
    {
        var bookings = await _context.Bookings.Include(b => b.User).Include(b => b.Car).ToListAsync();
        return bookings;
    }


    public async Task<Booking> GetByReference(string reference)
    {
        var booking = await _context.Bookings.Include(b => b.User).Include(b => b.Car).SingleOrDefaultAsync(b => b.BookingReference == reference);
        return booking;
    }

}
