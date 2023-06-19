using System.ComponentModel.DataAnnotations;

namespace CarRentals.Models.Car
{
    public class UpdateCarViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Enter the Name of your car ")]
        [MaxLength(25,ErrorMessage ="Enter not more than 25 characters!!!" )]
        public string Name { get; set; }
        [Required(ErrorMessage ="Enter the plateNumber Of the car")]
        [MaxLength(11,ErrorMessage ="Not Less than 11 characaters!!!")]
        public string PlateNumber { get; set; }
        [Display(Name = "Choose the cover photo of your car")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your car")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<CarGalleryModel> Gallery { get; set; }

    }
}
