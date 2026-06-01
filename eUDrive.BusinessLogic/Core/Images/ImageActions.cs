using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Image;

namespace eUDrive.BusinessLogic.Core.Images
{
    public class ImageActions
    {
        protected List<ImageDto> ExecuteGetProductImagesAction(int id)
        {
            using(var db = new ProductContext())
            {
                return db.ProductImgs
                    .Where(i => i.ProductId == id)
                    .Select(i => new ImageDto
                    {
                        Id = i.Id,
                        Url = i.Url
                    })
                    .ToList();
            }
        }

        protected ResponseMsg ExecuteAddProductImageAction(UploadImageDto image) 
        {
            if (image?.File == null || image.File.Length == 0)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "No file uploaded"
                };
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(image.File.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext))
                return new ResponseMsg { IsSuccess = false, Message = "Invalid file type" };

            var fileName = Guid.NewGuid() + ext;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                image.File.CopyTo(stream); 
            }

            
            using (var db = new ProductContext())
            {
                db.ProductImgs.Add(new ProductImgData
                {
                    ProductId = image.ProductId,
                    Url = $"/Images/{fileName}"
                });
                db.SaveChanges();
            }

            return new ResponseMsg 
            { 
                IsSuccess = true, 
                Message = "Image uploaded successfully"
            };

        }

        protected ResponseMsg ExecuteDeleteProductImageAction(int productId, int id)
        {
            using (var db = new ProductContext()) 
            {
                var existingImage = db.ProductImgs.FirstOrDefault(i => i.Id == id && i.ProductId == productId);

                if(existingImage == null) 
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Image not found for this product"
                    };
                }

                var url = existingImage.Url ?? string.Empty;
                var fileName = Path.GetFileName(url);

                string? fullPath = null;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
                }

                db.ProductImgs.Remove(existingImage);
                db.SaveChanges();

                if (!string.IsNullOrWhiteSpace(fullPath) && File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Image was deleted successfully"
            };
        }
    }
}
