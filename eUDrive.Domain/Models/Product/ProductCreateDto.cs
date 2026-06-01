namespace eUDrive.Domains.Models.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public int? H { get; set; }
        public int? W { get; set; }
        public int? L { get; set; }
    }
}
