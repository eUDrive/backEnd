using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("getAll")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
