using Microsoft.AspNetCore.Authorization;
using eUDrive.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using eUDrive.Domains.Entities.Order;

namespace eUDrive.Api.Controller
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private IOrderActions _order;

        public OrderController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _order = bl.GetOrderActions();
        }

        [HttpPost("cart/add")]
        public IActionResult AddToCart([FromBody] CartItemRequest request)
        {
            var result = _order.AddToCartAction(request.UserId, request.Item, request.CurrentPrice);
            return Ok(result);
        }

        [HttpGet("cart/{userId}")]
        public IActionResult GetCart(int userId)
        {
            var cart = _order.GetCartAction(userId);
            if (cart == null)
                return NotFound(new { message = "Cart is empty" });
            return Ok(cart);
        }

        [HttpPost("checkout/{userId}")]
        public IActionResult Checkout(int userId)
        {
            var result = _order.CheckoutAction(userId);
            return Ok(result);
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetOrderHistory(int userId)
        {
            var orders = _order.GetOrderHistoryAction(userId);
            return Ok(orders);
        }

        [HttpDelete("cart/item/{itemId}")]
        public IActionResult RemoveFromCart(int itemId)
        {
            var result = _order.RemoveFromCartAction(itemId);
            return Ok(result);
        }

        [HttpPut("cart/item/{itemId}/quantity")]
        public IActionResult UpdateQuantity(int itemId, [FromBody] QuantityRequest request)
        {
            var result = _order.UpdateCartItemQuantityAction(itemId, request.Quantity);
            return Ok(result);
        }
    }
}
