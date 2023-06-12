using Org.BouncyCastle.Bcpg.OpenPgp;

namespace CarRentals.Entities
{
    public class Payment : BaseEntity
    {
        public string PaymentReference { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
       public string BookingId { get; set; }
        public Booking Bookings { get; set; }
        public string PaymentCategoryId { get; set; }
        public PaymentCategory PaymentCategory { get; set; }
    }
}
