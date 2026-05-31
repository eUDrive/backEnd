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
                Description = dto.Description != null
                    ? new ProductDescriptionData { Description = dto.Description }
                    : null,
            };

            //Here we make id = 0 because in onther way db will send an error. Ef by himself will put id
            if (product.Description != null)
            {
                product.Description.Id = 0;

                if (product.Description.DescriptionAdvanced != null)
                    product.Description.DescriptionAdvanced.Id = 0;
            }

            if (product.Images != null)
            {
                foreach (var img in product.Images)
                {
                    img.Id = 0;
                    img.ProductId = 0; 
                }
            }

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
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Images = product.Images ?? new List<ProductImgData>(),
                    Price = product.Price,
                    Stock = product.Stock,
                    Status = ProductStatus.Active
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

        protected ResponseMsg ExecuteUpdateProductAction(ProductDto product)
        {
            using (var db = new ProductContext())
            {
                var existingProduct = db.Products.Include(p => p.Images).Include(p => p.Description).ThenInclude(d => d.DescriptionAdvanced)
                    .FirstOrDefault(p => p.Id == product.Id);

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
                //Here we also make id = 0 to not break something even if it will be send
                if (product.Description != null)
                {
                    if (existingProduct.Description == null)
                    {
                        product.Description.Id = 0;
                        if (product.Description.DescriptionAdvanced != null)
                            product.Description.DescriptionAdvanced.Id = 0;

                        existingProduct.Description = product.Description;
                    }
                    else
                    {
                        existingProduct.Description.Description = product.Description.Description;

                        if (product.Description.DescriptionAdvanced != null)
                        {
                            if (existingProduct.Description.DescriptionAdvanced == null)
                            {
                                product.Description.DescriptionAdvanced.Id = 0;
                                existingProduct.Description.DescriptionAdvanced = product.Description.DescriptionAdvanced;
                            }
                            else
                            {
                                existingProduct.Description.DescriptionAdvanced.H = product.Description.DescriptionAdvanced.H;
                                existingProduct.Description.DescriptionAdvanced.W = product.Description.DescriptionAdvanced.W;
                                existingProduct.Description.DescriptionAdvanced.L = product.Description.DescriptionAdvanced.L;
                            }
                        }
                    }
                }
                if (product.Stock >= 0) existingProduct.Stock = product.Stock;
                // I need to think what to do with images | Maybe I even will create separate function for it, but right now the most simple way
                if (product.Images != null)
                {
                    db.ProductImgs.RemoveRange(existingProduct.Images);
                    existingProduct.Images = product.Images;
                }

                if (product.CategoryId > 0) existingProduct.CategoryId = product.CategoryId;

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
