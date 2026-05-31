using eUDrive.DataAccess.Context;
using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Image;

namespace eUDrive.BusinessLogic.Core.Images
{
    public class ImageActions
    {
        private string? GetPath(string fileName) 
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);

            if (!File.Exists(path))
            {
                return null;
            }

            return path;
        }

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
    }
}
