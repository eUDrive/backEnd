using eUDrive.BusinessLogic.Core.Images;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Image;

namespace eUDrive.BusinessLogic.Functions.Images
{
    public class ImageFlow : ImageActions, IImageActions
    {
        public List<ImageDto> GetProductImagesAction(int id)
        {
            return ExecuteGetProductImagesAction(id);
        }

        public ResponseMsg AddProductImageAction(UploadImageDto image)
        {
            return ExecuteAddProductImageAction(image);
        }
    }
}
