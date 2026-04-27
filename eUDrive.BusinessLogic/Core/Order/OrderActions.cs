using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Order;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Core.Order
{
    public class OrderAction
    {
        protected List<OrderDto> ExecuteGetAllOrdersAction()
        {
            using (var db = new OrderContext())
            {
                return db.Orders
                    .Select(o => new OrderDto
                    {
                        Id = o.Id,
                        UserId = o.UserId,
                        Type = o.Type,
                        Name = o.Name,
                        CreatedAt = o.CreatedAt,
                    })
                    .ToList();
            }
        }

        protected OrderDto ExecuteGetOrderByIdAction(int id)
        {
            using (var db = new OrderContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == id);

                if (order == null) return null;

                return new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    Type = order.Type,
                    Name = order.Name,
                    CreatedAt = order.CreatedAt,
                };
            }
        }

        protected List<OrderDto> ExecuteGetOrdersByUserIdAction(int userId)
        {
            using (var db = new OrderContext())
            {
                return db.Orders
                    .Where(o => o.UserId == userId && o.IsActive == true)
                    .Select(o => new OrderDto
                    {
                        Id = o.Id,
                        UserId = o.UserId,
                        Type = o.Type,
                        Name = o.Name,
                        CreatedAt = o.CreatedAt,
                    })
                    .ToList();
            }
        }

        protected ResponseMsg ExecuteCreateOrderAction(OrderDto order)
        {
            if (order == null)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Order can't be empty"
                };
            }

            if (order.UserId <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "User ID is required" 
                };
            }  

            if (string.IsNullOrWhiteSpace(order.Name))
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Order name is required" 
                };
            }

            using (var db = new OrderContext())
            {
                var orderData = new OrderData
                {
                    UserId = order.UserId,
                    Type = order.Type,
                    Name = order.Name,
                    CreatedAt = order.CreatedAt,
                    IsActive = true,
                };

                db.Orders.Add(orderData);
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Order created successfully"
                };
            }
        }

        protected ResponseMsg ExecuteUpdateOrderAction(OrderDto order)
        {
            if (order == null)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Order can't be empty"
                };
            }

            if (order.UserId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "User ID is required"
                };
            }

            if (string.IsNullOrWhiteSpace(order.Name))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Order name is required"
                };
            }

            using (var db = new OrderContext())
            {
                var existingOrder = db.Orders.FirstOrDefault(o => o.Id == order.Id);
                if (existingOrder == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Order not found",
                    };
                }

                existingOrder.Name = order.Name;
                existingOrder.Type = order.Type;
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Order updated with success",
                };
            }
        }

        protected ResponseMsg ExecuteDeleteOrderAction(int id)
        {
            using (var db = new OrderContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == id);

                if (order == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Order not found"
                    };
                }

                order.IsActive = false;
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Order deleted successfully"
                };
            }
        }
    }
}
