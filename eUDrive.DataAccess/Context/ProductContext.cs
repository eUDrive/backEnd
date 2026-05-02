using eUDrive.Domains.Entities.Product;
using eUDrive.Domains.Enums;
using Microsoft.EntityFrameworkCore;

namespace eUDrive.DataAccess.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductData> Products { get; set; }
        public DbSet<ProductImgData> ProductImgs { get; set; }
        public DbSet<ProductDescriptionData> Description { get; set; }
        public DbSet<DescriptionAdvanced> DescriptionAdvanced { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSession.ConnectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DescriptionAdvanced>().ToTable("DescriptionAdvanced");
        }
    }
}