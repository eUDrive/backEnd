using Microsoft.AspNetCore.Http;

namespace eUDrive.Domains.Models.Image
{
    public class UploadImageDto
    {
        public int ProductId { get; set; }
        public IFormFile File { get; set; }
    }
}
