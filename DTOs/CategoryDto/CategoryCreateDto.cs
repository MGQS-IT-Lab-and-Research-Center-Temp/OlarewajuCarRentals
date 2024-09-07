using System.ComponentModel.DataAnnotations;

namespace CarRentals.DTOs.CategoryDto
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
