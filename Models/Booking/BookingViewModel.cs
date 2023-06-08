namespace CarRentals.Models.Booking
{
    public class BookingViewModel
    {
        public string Id { get; set; }
        public string CarId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string BookingReference { get; set; }
        public string PaymentReference { get; set; }
    }
}
