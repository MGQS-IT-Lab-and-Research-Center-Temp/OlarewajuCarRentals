namespace CarRentals.Entities
{
     public class Booking : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
        public bool IsPaid { get; set; }
        public  string BookingReference { get; set; }
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
