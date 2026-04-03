using eUDrive.Api.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new();
        private static int _nextId = 1;

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id) 
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product) 
        {
            product.Id = _nextId++;

            _products.Add(product);

            return Created($"/api/product/{product.Id}", product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;

            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            _products.Remove(product);
            return NoContent();
        }
    }
}
