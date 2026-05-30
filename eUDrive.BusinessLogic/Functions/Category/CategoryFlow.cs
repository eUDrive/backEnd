using eUDrive.BusinessLogic.Core.Category;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Category;

namespace eUDrive.BusinessLogic.Functions.Category
{
    public class CategoryFlow : CategoryActions, ICategoryActions
    {
        public List<CategoryDto> GetAllCategoriesAction()
        {
            return ExecuteGetAllCategoriesAction();
        }

        public CategoryDto? GetCategoryByIdAction(int id)
        {
            return ExecuteGetCategoryByIdAction(id);
        }

        public ResponseMsg CreateCategoryAction(CategoryDto category)
        {
            return ExecuteCreateCategoryAction(category);
        }

        public ResponseMsg UpdateCategoryAction(CategoryDto category)
        {
            return ExecuteUpdateCategoryAction(category);
        }

        public ResponseMsg DeleteCategoryAction(int id)
        {
            return ExecuteDeleteCategoryAction(id);
        }
    }
}
