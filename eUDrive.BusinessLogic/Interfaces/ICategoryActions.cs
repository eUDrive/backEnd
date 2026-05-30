using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Category;

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface ICategoryActions
    {
        List<CategoryDto> GetAllCategoriesAction();
        CategoryDto? GetCategoryByIdAction(int id);
        ResponseMsg CreateCategoryAction(CategoryDto category);
        ResponseMsg UpdateCategoryAction(CategoryDto category);
        ResponseMsg DeleteCategoryAction(int id);
    }
}
