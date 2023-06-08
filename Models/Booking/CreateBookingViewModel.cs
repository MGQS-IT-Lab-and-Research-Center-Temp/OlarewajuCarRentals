namespace CarRentals.Models.Booking
{
    public class CreateBookingViewModel
    {
        public string CarId { get; set; }
        public string UserId { get; set; }
        public string CarCoverImageURL { get; set; }
        public string PaymentId { get; set; }
    }
}
