using eUDrive.Domains.Models.Order;

namespace eUDrive.Domains.Entities.Order
{
    public class CartItemRequest
    {
        public int UserId { get; set; }
        public OrderItemDto Item { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
