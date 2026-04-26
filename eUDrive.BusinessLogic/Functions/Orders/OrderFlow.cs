using eUDrive.BusinessLogic.Core.Products;
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

        public new OrderDto GetOrderByIdAction(int id)
        {
            return base.GetOrderByIdAction(id);
        }

        public ResponseMsg CreateOrderAction(OrderDto order)
        {
            return ExecuteCreateOrderAction(order);
        }

        public ResponseMsg UpdateOrderAction(OrderDto order)
        {
            return ExecuteUpdateOrderAction(order);
        }

        public ResponseMsg DeleteOrdertAction(int id)
        {
            return ExecuteDeleteOrderAction(id);
        }
    }
}