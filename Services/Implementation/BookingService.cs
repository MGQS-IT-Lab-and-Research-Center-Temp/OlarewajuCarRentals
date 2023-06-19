using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Booking;
using CarRentals.Repository.Interfaces;
using CarRentals.Services.Interfaces;
using System.Security.Claims;

namespace CarRentals.Services.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public BaseResponseModel BookCar(CreateBookingViewModel model)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var LoggedInuser = _unitOfWork.Users.Get(userIdClaim);
            if (LoggedInuser is null)
            {
                response.Message = "User not found";
                return response;
            }
            var car = _unitOfWork.Cars.GetCar(c => c.Id == model.CarId);

            if (car is null)
            {
                response.Message = "car not found";
                return response;
            }


            if(car.AailabilityStaus is false)
            {
                response.Message = "car is unavailable";
                return response;
            }

            var booking = new Booking
            {
                CarId = model.CarId,
                Car = car,
                UserId = model.UserId,
                User = LoggedInuser,
                BookingReference = $"{Guid.NewGuid()}",
                BookedTime = model.BookedTime,
                ReturnTime = model.ReturnTime
            };
            try
            {
                car.AailabilityStaus = false;
                _unitOfWork.Bookings.Create(booking);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Car  Booked successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to book car . {ex.Message}";
                return response;
            }
        }

        public BookingResponseModel GetBooking(string id)
        {
            var response = new BookingResponseModel();
            var bookingExist = _unitOfWork.Bookings.Exists(c => c.Id == id);

            if (!bookingExist)
            {
                response.Message = $"Booking does not exist.";
                return response;
            }

            var booking = _unitOfWork.Bookings.GetBooking(bk=>bk.Id == id);

            response.Message = "Success";
            response.Status = true;
            response.Data = new BookingViewModel
            {
                Id = booking.Id,
                CarId = booking.CarId,
                UserId = booking.UserId,
                CarName = booking.Car.Name,
                UserName = $"{booking.User.FirstName} {booking.User.LastName}",
                BookingReference = booking.BookingReference,
            };

            return response;
        }

        public BookingsResponseModel GetBookings()
        {
            var response = new BookingsResponseModel();

            var bookings = _unitOfWork.Bookings.GetAllBookings(c => c.IsDeleted == false);

            if (bookings.Count == 0)
            {
                response.Message = "No comments yet!";
                return response;
            }

            response.Data = bookings
                    .Select(bk => new BookingViewModel
                    {
                        Id = bk.Id,
                        CarId = bk.CarId,
                        UserId = bk.UserId,
                        UserName = $"{bk.User.FirstName} {bk.User.LastName}",
                        CarName= bk.Car.Name
                    }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public BookingResponseModel GetByReference(string reference)
        {
            var response = new BookingResponseModel();
            var bookingExist = _unitOfWork.Bookings.Exists(bk => bk.BookingReference == reference);

            if (!bookingExist)
            {
                response.Message = $"Booking does not exist.";
                return response;
            }

            var booking = _unitOfWork.Bookings.GetByReference(reference);

            response.Message = "Success";
            response.Status = true;
            response.Data = new BookingViewModel
            {
                Id = booking.Id,
                CarId = booking.CarId,
                UserId = booking.UserId,
                CarName = booking.Car.Name,
                UserName = $"{booking.User.FirstName} {booking.User.LastName}",
                BookingReference = booking.BookingReference,
            };

            return response;
        }
    }
}
