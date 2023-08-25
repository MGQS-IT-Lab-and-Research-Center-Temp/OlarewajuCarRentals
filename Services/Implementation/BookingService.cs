﻿using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Booking;
using CarRentals.Repository.Interfaces;
using CarRentals.Services.Interfaces;
using System.Linq.Expressions;
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
        public async Task<BaseResponseModel> BookCar(CreateBookingViewModel model)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var LoggedInuser = await _unitOfWork.Users.GetAsync(userIdClaim);
            if (LoggedInuser is null)
            {
                response.Message = "User not found";
                return response;
            }
            var car = await _unitOfWork.Cars.GetCar(c => c.Id == model.CarId);

            if (car is null)
            {
                response.Message = "car not found";
                return response;
            }


            if (car.AailabilityStaus is false)
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
                await _unitOfWork.Bookings.CreateAsync(booking);
                await _unitOfWork.SaveChangesAsync();
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

        public async Task<BaseResponseModel> GetBooking(string id)
        {
            var response = new BookingResponseModel();
            var bookingExist = await _unitOfWork.Bookings.ExistsAsync(c => c.Id == id);

            if (!bookingExist)
            {
                response.Message = $"Booking does not exist.";
                return response;
            }

            var booking = await _unitOfWork.Bookings.GetBooking(bk => bk.Id == id);

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

        public async Task<BaseResponseModel> GetBookings()
        {
            var response = new BookingsResponseModel();
            var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");

            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Expression<Func<Booking, bool>> expression = bk => (bk.UserId == userIdClaim && bk.IsDeleted == false);

            var bookings = IsInRole ? await _unitOfWork.Bookings.GetAllBookings() : await _unitOfWork.Bookings.GetAllBookings(expression);

            if (bookings.Count == 0)
            {
                response.Message = "No Bookings yet!";
                return response;
            }

            response.Data = bookings
                    .Select(bk => new BookingViewModel
                    {
                        Id = bk.Id,
                        CarId = bk.CarId,
                        UserId = bk.UserId,
                        UserName = $"{bk.User.FirstName} {bk.User.LastName}",
                        CarName = bk.Car.Name
                    }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public async Task<BaseResponseModel> GetByReference(string reference)
        {
            var response = new BookingResponseModel();
            var bookingExist = await _unitOfWork.Bookings.ExistsAsync(bk => bk.BookingReference == reference);

            if (!bookingExist)
            {
                response.Message = $"Booking does not exist.";
                return response;
            }

            var booking = await _unitOfWork.Bookings.GetByReference(reference);

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
