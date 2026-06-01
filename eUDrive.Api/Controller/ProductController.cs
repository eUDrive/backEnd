using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private IProductActions _productActions;

        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _productActions = bl.GetProductActions();
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetAllProducts()
        {
            var products = _productActions.GetAllProductsAction();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetProductById(int id)
        {
            var product = _productActions.GetProductByIdAction(id);
            if (product == null) return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct([FromBody] ProductCreateDto product)
        {
            var result = _productActions.CreateProductAction(product);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductDto product)
        {
            var result = _productActions.UpdateProductAction(id, product);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productActions.DeleteProductAction(id);

            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }
}
