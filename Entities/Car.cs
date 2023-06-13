namespace CarRentals.Entities
{

    public class Car : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PlateNumber { get; set; }
        public string CoverImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool AailabilityStaus { get; set; }  
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<CarCategory> CarCategories { get; set; } = new HashSet<CarCategory>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<CarGallery> CarGalleries { get; set; } = new HashSet<CarGallery>();

    }
}
