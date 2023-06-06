using CarRentals.Models.CarGallery;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRentals.Models.Car
{
    public class UpdateCarViewModel
    {
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<CarGalleryModel> Gallery { get; set; }

    }
}
