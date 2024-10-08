﻿using CarRentals.Entities;
using CarRentals.Models.Category;
using CarRentals.Models;
using CarRentals.Repository.Interface;
using CarRentals.Repository.Interfaces;
using CarRentals.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace CarRentals.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseModel> CreateCategory(CreateCategoryViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var isCategoryExist = await _unitOfWork.Categories.ExistsAsync(c => c.Name == request.Name);

            if (isCategoryExist)
            {

                response.Message = "Category already exist!";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                response.Message = "Category name is required!";
                return response;
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                CreatedBy = createdBy
            };

            try
            {
                await _unitOfWork.Categories.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                response.Status = true;
                response.Message = "Category created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create category at this time: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponseModel> DeleteCategory(string categoryId)
        {
            var response = new BaseResponseModel();
            var isCategoryExist = await _unitOfWork.Categories.ExistsAsync(c => c.Id == categoryId && !c.IsDeleted);

            if (!isCategoryExist)
            {
                response.Message = "Category does not exist.";
                return response;
            }

            var category = await _unitOfWork.Categories.GetAsync(categoryId);
            category.IsDeleted = true;

            try
            {
                await _unitOfWork.Categories.RemoveAsync(category);
                await _unitOfWork.SaveChangesAsync();
                response.Status = true;
                response.Message = "Category successfully deleted.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Can not delete category: {ex.Message}";
                return response;
            }
        }

        public async Task<CategoriesResponseModel> GetAllCategory()
        {
            var response = new CategoriesResponseModel();

            try
            {
                Expression<Func<Category, bool>> expression = c => c.IsDeleted == false;
                var category = await _unitOfWork.Categories.GetAllAsync(expression);

                if (category is null || category.Count == 0)
                {
                    response.Message = "No categories found!";
                    return response;
                }

                response.Data = category.Select(
                    category => new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description
                    }).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public async Task<CategoryResponseModel> GetCategory(string categoryId)
        {
            var response = new CategoryResponseModel();

            Expression<Func<Category, bool>> expression = c =>
                                                (c.Id == categoryId)
                                                && (c.Id == categoryId
                                                && c.IsDeleted == false);

            var categoryExist = await _unitOfWork.Categories.ExistsAsync(expression);

            if (!categoryExist)
            {
                response.Message = $"Category with id {categoryId} does not exist.";
                return response;
            }

            var category = await _unitOfWork.Categories.GetAsync(categoryId);

            response.Message = "Success";
            response.Status = true;
            response.Data = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return response;
        }

        public async Task<BaseResponseModel> UpdateCategory(string categoryId, UpdateCategoryViewModel request)
        {
            var response = new BaseResponseModel();
            string modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var categoryExist = await _unitOfWork.Categories.ExistsAsync(c => c.Id == categoryId);

            if (!categoryExist)
            {
                response.Message = "Category does not exist.";
                return response;
            }

            var category = await _unitOfWork.Categories.GetAsync(categoryId);
            category.Description = request.Description;
            category.ModifiedBy = modifiedBy;

            try
            {
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                response.Message = "Category updated successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the category: {ex.Message}";
                return response;
            }
        }

        public async Task<IEnumerable<SelectListItem>> SelectCategories()
        {
            var categories = await _unitOfWork.Categories.SelectAll();

            return categories.Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id
            });
        }
    }
}
