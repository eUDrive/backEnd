using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Enums;

namespace eUDrive.Domains.Models.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductDescriptionData? Description { get; set; }
        public ProductCategory Category { get; set; }
        public List<ProductImgData> Images { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
