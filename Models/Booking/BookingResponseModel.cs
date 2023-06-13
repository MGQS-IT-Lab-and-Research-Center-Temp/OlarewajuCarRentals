using CarRentals.Models.Car;

namespace CarRentals.Models.Booking
{
    public class BookingResponseModel :BaseResponseModel
    {

        public BookingViewModel Data { get; set; }
    }
    public class BookingsResponseModel : BaseResponseModel
    {
        public List<BookingViewModel> Data { get; set; }
    }
}
