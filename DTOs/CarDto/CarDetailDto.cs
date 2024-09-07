using CarRentals.Models.Comment;
using CarRentals.Models;

namespace CarRentals.DTOs.CarDto
{
    public class CarDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CoverImageURL { get; set; }
        public bool AvailabilityStatus { get; set; }
        public decimal Price { get; set; }
        public string PlateNumber { get; set; }
        public List<CarGalleryModel> CarGalleries { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
