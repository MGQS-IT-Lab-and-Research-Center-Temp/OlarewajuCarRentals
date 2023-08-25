using AspNetCoreHero.ToastNotification.Abstractions;
using CarRentals.Models.Booking;
using CarRentals.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentals.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly INotyfService _notyf;
        public BookingController(IBookingService bookingService, INotyfService notyf)
        {

            _bookingService = bookingService;
            _notyf = notyf;
        }
        public async Task<IActionResult> Index()
        {
            var cars = await _bookingService.GetBookings();
            ViewData["Message"] = cars.Message;
            ViewData["Status"] = cars.Status;

            return View(cars.Data);
        }
        [HttpGet]
        public IActionResult BookCar()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookCar(CreateBookingViewModel model)
        {

            var response = await _bookingService.BookCar(model);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> SuccessPage(string id)
        {
            var booking = await _bookingService.GetBooking(id);
            ViewData["Message"] = booking.Message;
            ViewData["Status"] = booking.Status;

            return View(booking.Data);

        }
    }
}
