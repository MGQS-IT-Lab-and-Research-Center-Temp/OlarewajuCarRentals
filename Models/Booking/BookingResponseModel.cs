using CarRentals.Models.Car;

namespace CarRentals.Models.Booking
{
    public class BookingResponseModel
    {

        public BookingViewModel Data { get; set; }
    }
    public class BookingssResponseModel : BaseResponseModel
    {
        public List<BookingViewModel> Data { get; set; }
    }
}
