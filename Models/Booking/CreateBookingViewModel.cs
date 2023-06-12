namespace CarRentals.Models.Booking
{
    public class CreateBookingViewModel
    {
        public string CarId { get; set; }
        public string UserId { get; set; }
        public DateTime BookedTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public string CarCoverImageURL { get; set; }
    }
}
