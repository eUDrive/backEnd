using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eUDrive.Domains.Entities.Product
{
    public class ProductImgData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Url { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public ProductData Product { get; set; }
    }
}
