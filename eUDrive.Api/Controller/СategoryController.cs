using eUDrive.DataAccess.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetAllCategories()
        {
            using (var db = new ProductContext())
            {
                var categories = db.Categories.ToList();
                return Ok(categories);
            }
        }
    }
}
