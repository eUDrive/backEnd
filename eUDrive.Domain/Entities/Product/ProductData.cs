using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eUDrive.Domains.Enums;

namespace eUDrive.Domains.Entities.Product
{
    public class ProductData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ProductDescriptionData? Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public int Stock {  get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryData Category { get; set; }

        [InverseProperty("Product")]
        public List<ProductImgData> Images { get; set; } = new();

        public ProductStatus Status { get; set; } = ProductStatus.Active;
    }
}
