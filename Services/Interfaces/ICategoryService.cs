using CarRentals.Models.Category;
using CarRentals.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRentals.Services.Interfaces
{
    public interface ICategoryService
    {

        BaseResponseModel CreateCategory(CreateCategoryViewModel createCategoryDto);
        BaseResponseModel DeleteCategory(string categoryId);
        BaseResponseModel UpdateCategory(string categoryId, UpdateCategoryViewModel updateCategoryDto);
        CategoryResponseModel GetCategory(string categoryId);
        CategoriesResponseModel GetAllCategory();
        IEnumerable<SelectListItem> SelectCategories();
    }
}
