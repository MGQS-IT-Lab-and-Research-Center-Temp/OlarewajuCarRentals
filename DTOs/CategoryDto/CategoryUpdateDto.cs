using System.ComponentModel.DataAnnotations;

namespace CarRentals.DTOs.CategoryDto
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
