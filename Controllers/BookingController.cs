using AspNetCoreHero.ToastNotification.Abstractions;
using CarRentals.Models.Booking;
using CarRentals.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentals.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly INotyfService _notyf;
        public BookingController(IBookingService bookingService, INotyfService notyf)
        {

            _bookingService = bookingService;
            _notyf = notyf;
        }
        [HttpGet]
        public IActionResult BookCar()
        {

            return View();
        }

        [HttpPost]
        public IActionResult BookCar(CreateBookingViewModel model)
        {

            var response = _bookingService.BookCar(model);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }


        //public IActionResult SuccessPage(string id)
        //{
        //    var booking = _bookingService.GetBooking(id);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return View(booking.Data);
        //    }

        //}
    }
}
