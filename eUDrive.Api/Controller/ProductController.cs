using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductActions _productActions;

        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _productActions = bl.GetProductActions();
        }

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            var products = _productActions.GetAllProductsAction();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productActions.GetProductByIdAction(id);
            if (product == null) return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto product)
        {
            var result = _productActions.CreateProductAction(product);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto product)
        {
            product.Id = id;
            var result = _productActions.UpdateProductAction(product);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productActions.DeleteProductAction(id);

            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }
}
