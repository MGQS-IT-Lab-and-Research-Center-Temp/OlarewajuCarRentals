using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Booking;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BaseResponseModel> BookCar(CreateBookingViewModel model);

        Task<BookingResponseModel> GetBooking(string id);

        Task<BookingResponseModel> GetByReference(string reference);

        Task<BookingsResponseModel> GetBookings();
    }
}
