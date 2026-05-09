using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUDrive.Domains.Entities.Product
{
    public class ProductDescriptionData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public int ProductId {get; set;}
        [ForeignKey("ProductId")]
        public ProductData Product { get; set; }
        public DescriptionAdvanced? DescriptionAdvanced { get; set; }
    }
}
