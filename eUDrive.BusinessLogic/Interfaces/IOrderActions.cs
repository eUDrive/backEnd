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
        List<OrderDto> GetAllOrdersAction();
        OrderDto GetOrderByIdAction(int id);
        ResponseMsg CreateOrderAction(OrderDto product);
        ResponseMsg UpdateOrderAction(OrderDto product);
        ResponseMsg DeleteOrderAction(int id);
    }
}
