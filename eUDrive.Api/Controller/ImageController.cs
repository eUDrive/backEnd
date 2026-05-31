using eUDrive.BusinessLogic.Interfaces;
using eUDrive.Domains.Models.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUDrive.Api.Controller
{
    [Route("api/image")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private IImageActions _imageActions;

        public ImageController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _imageActions = bl.GetImageActions();
        }

        [HttpGet("{id}")]
        public IActionResult GetProductImages(int id) 
        {
            var result = _imageActions.GetProductImagesAction(id);

            return Ok(result);
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public IActionResult UploadImage([FromForm] UploadImageDto upload)
        {
            var result = _imageActions.AddProductImageAction(upload);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
