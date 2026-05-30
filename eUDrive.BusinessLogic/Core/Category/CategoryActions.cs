using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace eUDrive.BusinessLogic.Core.Category
{
    public class CategoryActions
    {
        protected List<CategoryDto> ExecuteGetAllCategoriesAction()
        {
            using (var db = new ProductContext())
            {
                return db.Categories
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
            }
        }

        protected CategoryDto? ExecuteGetCategoryByIdAction(int id)
        {
            using (var db = new ProductContext())
            {
                var category = db.Categories
                    .FirstOrDefault(c => c.Id == id);

                if (category == null)
                    return null;

                return new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
        }

        protected ResponseMsg ExecuteCreateCategoryAction(CategoryDto categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Category name cannot be empty"
                };
            }

            using (var db = new ProductContext())
            {
                var existingCategory = db.Categories
                    .FirstOrDefault(c => c.Name.ToLower() == categoryDto.Name.ToLower());

                if (existingCategory != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Category with name '{categoryDto.Name}' already exists"
                    };
                }

                var newCategory = new CategoryData
                {
                    Name = categoryDto.Name
                };

                db.Categories.Add(newCategory);
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Category created successfully"
            };
        }

        protected ResponseMsg ExecuteUpdateCategoryAction(CategoryDto categoryDto)
        {
            if (categoryDto.Id <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Invalid category ID"
                };
            }

            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Category name cannot be empty"
                };
            }

            using (var db = new ProductContext())
            {
                var existingCategory = db.Categories
                    .FirstOrDefault(c => c.Id == categoryDto.Id);

                if (existingCategory == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Category not found"
                    };
                }

                // Проверяем, что имя не используется другой категорией
                var duplicateName = db.Categories
                    .FirstOrDefault(c => c.Id != categoryDto.Id && 
                                       c.Name.ToLower() == categoryDto.Name.ToLower());

                if (duplicateName != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Category with name '{categoryDto.Name}' already exists"
                    };
                }

                existingCategory.Name = categoryDto.Name;
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Category updated successfully"
            };
        }

        protected ResponseMsg ExecuteDeleteCategoryAction(int id)
        {
            if (id <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Invalid category ID"
                };
            }

            using (var db = new ProductContext())
            {
                var category = db.Categories
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Category not found"
                    };
                }

                if (category.Products.Any())
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Cannot delete category that contains products"
                    };
                }

                db.Categories.Remove(category);
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Category deleted successfully"
            };
        }
    }
}
