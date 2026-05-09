using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Order;
using eUDrive.Domains.Enums;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace eUDrive.BusinessLogic.Core.Order
{
    public class OrderAction
    {
        protected ResponseMsg ExecuteAddToCartAction(int userId, OrderItemDto item, decimal currentPrice)
        {
            if (userId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "User ID is required",
                };
            }

            if (item.ItemId <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Item ID is required" 
                };
            }

            if (item.Quantity <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Quantity must be greater than 0" 
                };
            }

            using (var db = new OrderContext())
            {
                var cart = db.Orders.FirstOrDefault(o => o.UserId == userId && o.Status == OrderStatus.Pending);

                if (cart == null)
                {
                    cart = new OrderData
                    {
                        UserId = userId,
                        Status = OrderStatus.Pending,
                        TotalPrice = 0,
                        CreatedAt = DateTime.Now,
                    };

                    db.Orders.Add(cart);
                    db.SaveChanges();
                }

                var orderItem = new OrderItemData
                {
                    OrderId = cart.Id,
                    Type = item.Type,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    PriceAtPurchase = currentPrice,
                    CreatedAt = DateTime.Now,
                };

                db.OrderItems.Add(orderItem);
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Item added to cart successfully"
                };
            }
        }

        protected OrderDto ExecuteGetCartAction(int userId)
        {
            if (userId <= 0) return null;

            using (var db = new OrderContext())
            {
                var order = db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                    .FirstOrDefault();

                if (order == null) return null;

                return MapOrderToDto(order);
            }
        }

        protected List<OrderDto> ExecuteGetOrderHistoryAction(int userId)
        {
            if (userId <= 0) return new List<OrderDto>();

            using (var db = new OrderContext())
            {
                var orders = db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Completed)
                    .ToList();

                return orders.Select(o => MapOrderToDto(o)).ToList();
            }
        }

        protected ResponseMsg ExecuteCheckoutAction(int userId)
        {
            if (userId <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "User Id is required",
                };
            }

            using (var db = new OrderContext())
            {
                var cart = db.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                    .FirstOrDefault();

                if (cart == null)
                {
                    return new ResponseMsg 
                    { 
                        IsSuccess = false, 
                        Message = "Cart is empty" 
                    };
                }
                    

                if (!cart.OrderItems.Any())
                {
                    return new ResponseMsg 
                    { 
                        IsSuccess = false, 
                        Message = "No items in cart"
                    };
                }
                    

                cart.TotalPrice = cart.OrderItems.Sum(oi => oi.PriceAtPurchase * oi.Quantity);
                cart.Status = OrderStatus.Completed;

                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Order completed successfully"
                };
            }
        }

        protected ResponseMsg ExecuteRemoveFromCartAction(int orderItemId)
        {
            if (orderItemId <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Item ID is required" 
                };
            }

            using (var db = new OrderContext())
            {
                var item = db.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);

                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Item not found",
                    };
                }

                db.OrderItems.Remove(item);
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Item removed from cart",
                };
            }
        }

        protected ResponseMsg ExecuteUpdateCartItemQuantityAction(int orderItemId, int quantity)
        {
            if (orderItemId <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Item ID is required" 
                };
            }
                
            if (quantity <= 0)
            {
                return new ResponseMsg 
                { 
                    IsSuccess = false, 
                    Message = "Quantity must be greater than 0" 
                };
            }
              
            using (var db = new OrderContext())
            {
                var item = db.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);

                if (item == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Item not found",
                    };
                }

                item.Quantity = quantity;
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Quantity updated",
                };
            }
        }

        protected OrderDto MapOrderToDto(OrderData order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    Type = oi.Type,
                    ItemId = oi.ItemId,
                    Quantity = oi.Quantity,
                    PriceAtPurchase = oi.PriceAtPurchase,
                    CreatedAt = oi.CreatedAt,
                }).ToList() ?? new List<OrderItemDto>()
            };
        }
    }
}
