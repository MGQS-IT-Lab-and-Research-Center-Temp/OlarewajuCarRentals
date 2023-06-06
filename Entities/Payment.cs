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
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
