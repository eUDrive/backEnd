using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUDrive.Domains.Entities.Product
{
    public class CategoryData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public List<ProductData> Products { get; set; } = new();
    }
}
