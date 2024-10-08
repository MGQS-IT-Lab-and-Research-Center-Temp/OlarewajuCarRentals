﻿using CarRentals.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRentals.DTOs.CarDto
{
    public class CarCreateDto
    {

        [Required(ErrorMessage = "One or more Categories need to be selected")]

        public List<string> CategoryIds { get; set; }
        [Required(ErrorMessage = "Enter the Name of your car ")]
        [MaxLength(25, ErrorMessage = "Enter not more than 25 characters!!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the plateNumber Of the car")]
        [MaxLength(11, ErrorMessage = "Not Less than 11 characaters!!!")]
        public string PlateNumber { get; set; }
        [Display(Name = "Choose the cover photo of your car")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your car")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<CarGalleryModel> Gallery { get; set; }
        [Required(ErrorMessage = "Enter Price for renting!!!")]
        public decimal Price { get; set; }
    }
}
