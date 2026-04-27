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
        public List<OrderDto> GetAllOrdersAction()
        {
            return ExecuteGetAllOrdersAction();
        }

        public OrderDto GetOrderByIdAction(int id)
        {
            return ExecuteGetOrderByIdAction(id);
        }
        public List<OrderDto> GetOrdersByUserIdAction(int userId)
        {
            return ExecuteGetOrdersByUserIdAction(userId);
        }

        public ResponseMsg CreateOrderAction(OrderDto order)
        {
            return ExecuteCreateOrderAction(order);
        }

        public ResponseMsg UpdateOrderAction(OrderDto order)
        {
            return ExecuteUpdateOrderAction(order);
        }

        public ResponseMsg DeleteOrderAction(int id)
        {
            return ExecuteDeleteOrderAction(id);
        }
    }
}