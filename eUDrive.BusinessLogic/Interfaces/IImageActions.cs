using eUDrive.Domains.Models.Base;
using eUDrive.Domains.Models.Image;

namespace eUDrive.BusinessLogic.Interfaces
{
    public interface IImageActions
    {
        List<ImageDto> GetProductImagesAction(int id);

        ResponseMsg AddProductImageAction(UploadImageDto image);
    }
}
