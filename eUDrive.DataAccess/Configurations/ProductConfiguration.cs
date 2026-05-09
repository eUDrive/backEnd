using eUDrive.Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eUDrive.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductData>
    {
        public void Configure(EntityTypeBuilder<ProductData> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Description)
                .WithOne(d => d.Product)
                .HasForeignKey<ProductDescriptionData>(d => d.ProductId)
                .IsRequired(false);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
