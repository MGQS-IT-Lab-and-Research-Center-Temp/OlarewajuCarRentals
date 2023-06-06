using static System.Reflection.Metadata.BlobBuilder;

namespace CarRentals.Entities
{
    public class CarGallery : BaseEntity
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public Car Car { get; set; }
    }
}
