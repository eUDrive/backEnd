using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Product;
using eUDrive.Domains.Enums;
using Microsoft.EntityFrameworkCore;

namespace eUDrive.BusinessLogic.Core.Products
{
    public class ProductAction
    {
        protected List<ProductDto> ExecuteGetAllProductsAction()
        {
            using (var db = new ProductContext())
            {
                return db.Products
                    .Include(p => p.Images)
                    .Include(p => p.Description).ThenInclude(d => d.DescriptionAdvanced)
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        Images = p.Images,
                        Price = p.Price,
                        Stock = p.Stock,
                        Status = p.Status
                    })
                    .ToList();
            }
        }

        protected ProductDto? ExecuteGetProductByIdAction(int id) 
        {
            using (var db = new ProductContext())
            {
                var product = db.Products.Include(p => p.Images).Include(p => p.Description).ThenInclude(d => d.DescriptionAdvanced)
                    .FirstOrDefault(p => p.Id == id);

                if (product == null) return null;

                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Images = product.Images,
                    Price = product.Price,
                    Stock = product.Stock,
                    Status = product.Status
                };
            }
        }

        protected ResponseAction<ProductDto> ExecuteCreateProductAction(ProductCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return new ResponseAction<ProductDto>
                {
                    IsSuccess = false,
                    Message = "Product name can't be empty"
                };
            }

            if (dto.Price <= 0)
            {
                return new ResponseAction<ProductDto>
                {
                    IsSuccess = false,
                    Message = "Product price must be greater than 0"
                };
            }

            var product = new ProductDto
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                Description = dto.Description == null ? null : new ProductDescriptionData
                {
                    Description = dto.Description,
                    DescriptionAdvanced = dto.H.HasValue &&
                                          dto.W.HasValue &&
                                          dto.L.HasValue
                        ? new DescriptionAdvanced
                        {
                            H = dto.H.Value,
                            W = dto.W.Value,
                            L = dto.L.Value
                        }
                        : null
                }
            };

            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.FirstOrDefault(p => p.Name.ToLower() == product.Name.ToLower() && (p.Status == ProductStatus.Active || p.Status == ProductStatus.Sold));

                if (existingProduct != null)
                {
                    return new ResponseAction<ProductDto>
                    {
                        IsSuccess = false,
                        Message = "Product with this name already exists"
                    };
                }

                var productData = new ProductData
                {
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Stock = product.Stock,
                    Status = ProductStatus.Active,
                    Description = dto.Description == null ? null : new ProductDescriptionData
                    {
                        Description = dto.Description,
                        DescriptionAdvanced = dto.H.HasValue &&
                                          dto.W.HasValue &&
                                          dto.L.HasValue
                        ? new DescriptionAdvanced
                        {
                            H = dto.H.Value,
                            W = dto.W.Value,
                            L = dto.L.Value
                        }
                        : null
                    }
                };

                db.Products.Add(productData);
                db.SaveChanges();

                product.Id = productData.Id;

                return new ResponseAction<ProductDto>
                {
                    IsSuccess = true,
                    Message = "Product created successfully",
                    Data = product
                };
            }
        }

        protected ResponseMsg ExecuteUpdateProductAction(int id, UpdateProductDto product)
        {
            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.Include(p => p.Description).ThenInclude(d => d.DescriptionAdvanced)
                    .FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Product not found"
                    };
                }

                if (!string.IsNullOrWhiteSpace(product.Name)) existingProduct.Name = product.Name;
                if (product.Price.HasValue && product.Price.Value > 0) existingProduct.Price = product.Price.Value;
                if (product.Stock.HasValue && product.Stock.Value >= 0) existingProduct.Stock = product.Stock.Value;
                if (product.CategoryId.HasValue && product.CategoryId.Value > 0) existingProduct.CategoryId = product.CategoryId.Value;
                if (product.Status.HasValue) existingProduct.Status = product.Status.Value;
                if (product.Description != null)
                {
                    existingProduct.Description ??= new ProductDescriptionData();

                    existingProduct.Description.Description = product.Description;

                    if(product.H.HasValue && product.W.HasValue && product.L.HasValue) 
                    {
                        if(existingProduct.Description.DescriptionAdvanced == null) 
                        {
                            existingProduct.Description.DescriptionAdvanced = new DescriptionAdvanced();
                        }

                        existingProduct.Description.DescriptionAdvanced.H = product.H.Value;
                        existingProduct.Description.DescriptionAdvanced.W = product.W.Value;
                        existingProduct.Description.DescriptionAdvanced.L = product.L.Value;
                    }
                }

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

                product.Status = ProductStatus.Inactive;
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