using CarRentals.DTOs.BookingDto;
using CarRentals.Models.Car;

namespace CarRentals.Models.Booking
{
    public class BookingResponseModel :BaseResponseModel
    {

        public BookingDetailDto Data { get; set; }
    }
    public class BookingsResponseModel : BaseResponseModel
    {
        public List<BookingListDto> Data { get; set; }
    }
}
