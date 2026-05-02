using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUDrive.Domains.Entities.Product
{
    public class ProductDescriptionData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Description { get; set; }

        public DescriptionAdvanced? DescriptionAdvanced { get; set; }
    }
}
