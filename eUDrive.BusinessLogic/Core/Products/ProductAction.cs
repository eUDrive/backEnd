using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.BusinessLogic.Core.Products
{
    public class ProductAction
    {
        protected List<ProductDto> ExecuteGetAllProductsAction()
        {
            using (var db = new ProductContext())
            {
                return db.Products
                    .Where(p => p.IsActive)
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                    })
                    .ToList();
            }
        }

        public ProductDto GetProductByIdAction(int id) 
        {
            using (var db = new ProductContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id && p.IsActive);

                if (product == null) return null;

                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                };
            }
        }

        protected ResponseMsg ExecuteCreateProductAction(ProductDto product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Product name can't be empty"
                };
            }

            if (product.Price <= 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Product price must be greater than 0"
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
