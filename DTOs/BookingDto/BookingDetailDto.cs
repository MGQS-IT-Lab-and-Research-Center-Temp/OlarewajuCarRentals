namespace CarRentals.DTOs.BookingDto
{
    public class BookingDetailDto
    {
        public string Id { get; set; }
        public string CarId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CarName { get; set; }
        public DateTime BookedTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public string BookingReference { get; set; }
    }
}
