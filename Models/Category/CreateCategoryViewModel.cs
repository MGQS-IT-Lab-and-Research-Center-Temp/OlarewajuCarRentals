using System.ComponentModel.DataAnnotations;

namespace CarRentals.Models.Category;

public class CreateCategoryViewModel
{
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; }
    public string Description { get; set; }
}
