using CarRentals.Models.Category;
using CarRentals.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRentals.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<BaseResponseModel> CreateCategory(CreateCategoryViewModel createCategoryDto);
        Task<BaseResponseModel> DeleteCategory(string categoryId);
        Task<BaseResponseModel> UpdateCategory(string categoryId, UpdateCategoryViewModel updateCategoryDto);
        Task<CategoryResponseModel> GetCategory(string categoryId);
        Task<CategoriesResponseModel> GetAllCategory();
        Task<IEnumerable<SelectListItem>> SelectCategories();
    }
}
