using CarRentals.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarRentals.Models.Car
{
    public class CreateCarViewModel
    {
        [Required(ErrorMessage = "One or more Categories need to be selected")]

        public List<string> CategoryIds { get; set; }
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
        [Required(ErrorMessage = "Enter Price for renting!!!")]
        public decimal Price { get; set; }

    }
}
