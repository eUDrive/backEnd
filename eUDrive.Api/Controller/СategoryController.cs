using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private ICategoryActions _categoryActions;

        public CategoryController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _categoryActions = bl.GetCategoryActions();
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryActions.GetAllCategoriesAction();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetCategoryById(int id) 
        {
            var category = _categoryActions.GetCategoryByIdAction(id);
            if (category == null) { return BadRequest( new {Message = $"There is no category with id = {id}"}); }
            return Ok(category);
        }

        [HttpPost("")]
        [Authorize(Roles="Admin")]
        public IActionResult CreateCategory([FromBody] CategoryDto category) 
        {
            var result = _categoryActions.CreateCategoryAction(category);

            if (!result.IsSuccess) 
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto category) 
        {
            category.Id = id;
            var result = _categoryActions.UpdateCategoryAction(category);

            if (!result.IsSuccess) { return BadRequest(result); }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public IActionResult DeleteCategory(int id) 
        {
            var result = _categoryActions.DeleteCategoryAction(id);
            if (!result.IsSuccess) { return BadRequest(result); }
            return Ok(result);
        }
    }
}
