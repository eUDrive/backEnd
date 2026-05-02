using eUDrive.BusinessLogic.Core.Order;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Functions.Orders
{
    public class OrderFlow : OrderAction, IOrderActions
    {
        public ResponseMsg AddToCartAction(int userId, OrderItemDto item, decimal currentPrice)
        {
            return ExecuteAddToCartAction(userId, item, currentPrice);
        }

        public OrderDto GetCartAction(int userId) 
        {
            return ExecuteGetCartAction(userId);
        }

        public List<OrderDto> GetOrderHistoryAction(int userId)
        {
            return ExecuteGetOrderHistoryAction(userId);
        }

        public ResponseMsg CheckoutAction(int userId)
        {
            return ExecuteCheckoutAction(userId);
        }

        public ResponseMsg RemoveFromCartAction(int orderItemId)
        {
            return ExecuteRemoveFromCartAction(orderItemId);
        }

        public ResponseMsg UpdateCartItemQuantityAction(int orderItemId, int quantity)
        {
            return ExecuteUpdateCartItemQuantityAction(orderItemId, quantity);
        }
    }
}