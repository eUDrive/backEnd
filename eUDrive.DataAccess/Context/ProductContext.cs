using eUDrive.Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace eUDrive.DataAccess.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductData> Products { get; set; }
        public DbSet<ProductImgData> ProductImgs { get; set; }
        public DbSet<CategoryData> Categories {get; set;}
        public DbSet<ProductDescriptionData> Descriptions { get; set; }
        public DbSet<DescriptionAdvanced> DescriptionAdvanced { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductContext).Assembly);
        }
    }
}