using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eUDrive.Domains.Entities.Product
{
    public class DescriptionAdvanced
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int H { get; set; }
        public int W { get; set; }
        public int L { get; set; }

        public int DescriptionId { get; set; }
        [ForeignKey("DescriptionId")]
        [JsonIgnore]
        public ProductDescriptionData Description { get; set; }
    }
}