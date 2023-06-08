using CarRentals.Models.Car;

namespace CarRentals.Models.Booking
{
    public class BookingResponseModel
    {

        public BookingViewModel Data { get; set; }
    }
    public class CarsResponseModel : BaseResponseModel
    {
        public List<BookingViewModel> Data { get; set; }
    }
}
