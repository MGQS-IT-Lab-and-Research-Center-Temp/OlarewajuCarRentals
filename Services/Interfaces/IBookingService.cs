using CarRentals.Models;
using CarRentals.Models.Booking;

namespace CarRentals.Services.Interfaces
{
    public interface IBookingService
    {
        BaseResponseModel BookTicket(CreateBookingViewModel model);

        Bookin GetBooking(int id);

        BookingDTO GetByReference(string reference);

        IList<BookingDTO> GetBookings();
    }
}
