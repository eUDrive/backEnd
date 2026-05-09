using eUDrive.Api.Filters;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderActions _orderActions;

        public OrderController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _orderActions = bl.GetOrderActions();
        }

        [HttpGet("all")]
        public IActionResult GetAllOrders()
        {
            var orders = _orderActions.GetAllOrdersAction();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderActions.GetOrderByIdAction(id);
            if (order == null) return NotFound(new { message = "Order not found" });

            return Ok(order);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var orders = _orderActions.GetOrdersByUserIdAction(userId);
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto order)
        {
            var result = _orderActions.CreateOrderAction(order);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDto order)
        {
            order.Id = id;
            var result = _orderActions.UpdateOrderAction(order);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var result = _orderActions.DeleteOrderAction(id);

            if (!result.IsSuccess) return NotFound(result);

            return Ok(result);
        }
    }
}
