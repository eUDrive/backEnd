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
                        Name = o.Name,
                        CreatedAt = o.CreatedAt,
                    })
                    .ToList();
            }
        }

        protected OrderDto GetOrderByIdAction(int id)
        {
            using (var db = new OrderContext())
            {
                var order = db.Orders.FirstOrDefault(p => p.Id == id);

                if (order == null) return null;

                return new OrderDto
                {
                    Id = order.Id,
                    Name = order.Name,
                    CreatedAt = order.CreatedAt,
                };
            }
        }

        protected ResponseMsg ExecuteCreateOrderAction(OrderDto order)
        {
            if (string.IsNullOrWhiteSpace(order.Name))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Order name can't be empty"
                };
            }

            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.FirstOrDefault(p => p.Name.ToLower() == product.Name.ToLower() && p.IsActive);

                if (existingProduct != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product with this name already exists"
                    };
                }
            }

            var productData = new ProductData
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            using (var db = new ProductContext())
            {
                db.Products.Add(productData);
                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product created successfully"
            };
        }

        protected ResponseMsg ExecuteUpdateProductAction(ProductDto product)
        {
            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found"
                    };
                }

                if (!string.IsNullOrWhiteSpace(product.Name)) existingProduct.Name = product.Name;
                if (product.Price > 0) existingProduct.Price = product.Price;
                if (!string.IsNullOrWhiteSpace(product.Description)) existingProduct.Description = product.Description;
                if (product.Stock >= 0) existingProduct.Stock = product.Stock;

                db.SaveChanges();
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Product updated successfully"
            };
        }

        protected ResponseMsg ExecuteDeleteProductAction(int id)
        {
            using (var db = new ProductContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found"
                    };
                }

                product.IsActive = false;
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Product deleted successfully"
                };
            }
        }
    }
}
