using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Booking;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Services.Interfaces
{
    public interface IBookingService
    {
        BaseResponseModel BookCar(CreateBookingViewModel model);

        BookingResponseModel GetBooking(string id);

        BookingResponseModel GetByReference(string reference);
       
        BookingsResponseModel GetBookings();
    }
}
