using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface IOrderActions
    {
        ResponseMsg AddToCartAction(int userId, OrderItemDto item, decimal currentPrice);
        OrderDto GetCartAction(int userId);
        List<OrderDto> GetOrderHistoryAction(int userId);
        ResponseMsg CheckoutAction(int userId);
        ResponseMsg RemoveFromCartAction(int orderItemId);
        ResponseMsg UpdateCartItemQuantityAction(int orderItemId, int quantity);
    }
}
